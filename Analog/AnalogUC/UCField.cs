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
    public partial class UCField : UserControl
    {
        public UCField()
        {
            InitializeComponent();
        }
        public void SetDbIMetaList(List<MetaDb.Db> dbLists)
        {
            //List<MetaDb.DbInterface> dbInterfacesICatalog = dbInterfaces.Where(x => x.DbInterfaceTypeId == (int)MetaDb.Enums.DbInterfaceType.IMeta).ToList();
            //List<Common.IdName> idnames = new List<Common.IdName>();
            //foreach (var item in dbLists.Where(y => dbInterfacesICatalog.Exists(x => x.DbListId == y.Id)).OrderBy(x => x.Name))
            //{
            //    // Код интерфейса, а имя - БД.
            //    idnames.Add(new Common.IdName() { Id = dbInterfacesICatalog.FirstOrDefault(x => x.DbListId == item.Id).Id, Name = item.Name });
            //}
            dbIMeta.DataSource = dbLists;//.ToArray();
        }

        public Field Value
        {
            set
            {
                Clear();
                if (value != null)
                {
                    // 
                    infoLabel.Text = value.Id.ToString();
                    nameTextBox.Text = value.Name;
                    catalogIdTextBox.Text = value.CatalogId.ToString();

                    // DBINTERFACE COMBOBOX
                    dbIMeta.Position = -1;
                    if (value.CatalogDbInterfaceId > 0)
                    {
                        Common.IdName a = ((List<MetaDb.Db>)dbIMeta.DataSource).FirstOrDefault(x => x.Id == value.CatalogDbInterfaceId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует интерфейс базы данных id=" + value.CatalogDbInterfaceId);
                        dbInterfaceComboBox.SelectedItem = a;
                    }
                }
            }
            get
            {
                try
                {
                    return new Field()
                    {
                        Id = string.IsNullOrEmpty(infoLabel.Text) ? -1 : int.Parse(infoLabel.Text),
                        Name = nameTextBox.Text,
                        CatalogId = int.Parse(catalogIdTextBox.Text),
                        CatalogDbInterfaceId = ((Common.IdName)dbInterfaceComboBox.SelectedItem).Id
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
            nameTextBox.Text = catalogIdTextBox.Text = infoLabel.Text = null;
            dbInterfaceComboBox.SelectedIndex = -1;
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
