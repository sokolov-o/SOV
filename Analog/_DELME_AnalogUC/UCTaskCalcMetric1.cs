using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Analog
{
    public partial class UCTaskCalcMetric1 : UserControl
    {
        public UCTaskCalcMetric1()
        {
            InitializeComponent();

            ucIntDouble.SetColumnNames("Сдвиг (кол)", "Вес поля");
            timeWindowInterval.SetMinMax(new int[] { -100, 100 });
        }
        public void SetListActions(List<Action> actions)
        {
            actionBindingSource.DataSource = actions.OrderBy(x => x.Name).ToArray();
        }
        public void SetListDbLists(List<MetaDb.DbInterface> dbInterfaces, List<MetaDb.Db> dbLists)
        {
            List<MetaDb.DbInterface> dbInterfacesICatalog = dbInterfaces.Where(x => x.DbInterfaceTypeId == (int)MetaDb.Enums.DbInterfaceType.IMeta).ToList();
            List<Common.IdName> idnames = new List<Common.IdName>();
            foreach (var item in dbLists.Where(y => dbInterfacesICatalog.Exists(x => x.DbListId == y.Id)).OrderBy(x => x.Name))
            {
                // Код интерфейса, а имя - БД.
                idnames.Add(new Common.IdName() { Id = dbInterfacesICatalog.FirstOrDefault(x => x.DbListId == item.Id).Id, Name = item.Name });
            }
            dbInterfaceBindingSource.DataSource = idnames.ToArray();
        }

        public Field Value
        {
            set
            {
                Clear();
                if (value != null)
                {
                    // 
                    idTextBox.Text = value.Id.ToString();
                    nameTextBox.Text = value.Name;
                    catalogIdTextBox.Text = value.CatalogId.ToString();
                    yearSTextBox.Text = value.YearS.HasValue ? value.YearS.ToString() : null;
                    timeWindowInterval.SetInterval(value.TimeQBack, value.TimeQForward);

                    // ACTION COMBOBOX
                    actionBindingSource.Position = -1;
                    if (value.ActionId > 0)
                    {
                        Action a = ((Action[])actionBindingSource.DataSource).FirstOrDefault(x => x.Id == value.ActionId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует действие id=" + value.ActionId);
                        actionComboBox.SelectedItem = a;
                    }

                    // DBINTERFACE COMBOBOX
                    dbInterfaceBindingSource.Position = -1;
                    if (value.CatalogDbInterfaceId > 0)
                    {
                        Common.IdName a = ((Common.IdName[])dbInterfaceBindingSource.DataSource).FirstOrDefault(x => x.Id == value.CatalogDbInterfaceId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует интерфейс базы данных id=" + value.CatalogDbInterfaceId);
                        dbInterfaceComboBox.SelectedItem = a;
                    }

                    //
                    ucIntDouble.Fill(value.FieldTimeShiftWeights);
                }
            }
            get
            {
                try
                {
                    return new Field()
                    {
                        Id = string.IsNullOrEmpty(idTextBox.Text) ? -1 : int.Parse(idTextBox.Text),
                        Name = nameTextBox.Text,
                        ActionId = ((Action)actionComboBox.SelectedItem).Id,
                        YearS = string.IsNullOrEmpty(yearSTextBox.Text) ? null : (int?)int.Parse(yearSTextBox.Text),
                        CatalogId = int.Parse(catalogIdTextBox.Text),
                        CatalogDbInterfaceId = ((Common.IdName)dbInterfaceComboBox.SelectedItem).Id,
                        FieldTimeShiftWeights = ucIntDouble.GetIntDoubleList().ToArray(),
                        TimeQBack = (int)timeWindowInterval.GetInterval()[0],
                        TimeQForward = (int)timeWindowInterval.GetInterval()[1],
                        Climate = needClimateCheckBox.Checked ? ucClimate.Value : null
                    };

                }
                catch
                {
                    return null;
                }
            }
        }
        public void Clear()
        {
            idTextBox.Text = nameTextBox.Text = catalogIdTextBox.Text = null;
            actionComboBox.SelectedIndex = dbInterfaceComboBox.SelectedIndex = -1;
        }

        private void needClimateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ucClimate.Enabled = needClimateCheckBox.Checked;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Field value = Value;
                if (value == null)
                {
                    MessageBox.Show("Отсутствует значение для сохранения. Возможно, заполнены не всё обязательные поля формы." +
                        "\nИнформация не сохранена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (value.Id > 0)
                    DataManager.GetInstance().FieldRepository.Update(value);
                else
                    DataManager.GetInstance().FieldRepository.Insert(value);

                UCItemSaved?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public delegate void UCItemSavedEventHandler();
        public event UCItemSavedEventHandler UCItemSaved;

    }
}
