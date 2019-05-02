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
    public partial class UCTaskCalcMetric : UserControl
    {
        public UCTaskCalcMetric()
        {
            InitializeComponent();

            ucTimeShiftWeights.SetColumnNames("Сдвиг (кол)", "Вес поля");
            timeWindowInterval.SetMinMax(new int[] { -100, 100 });
        }

        public void SetActionsList(List<Action> actions)
        {
            actionBindingSource.DataSource = actions.OrderBy(x => x.Name).ToArray();
        }

        public void SetFieldsList(List<Field> fields)
        {
            List<Field> fieldsOrdered = fields.OrderBy(x => x.Name).ToList();
            fieldIniBindingSource.DataSource = fieldsOrdered;
            fieldAngBindingSource.DataSource = fieldsOrdered;
        }

        public TaskCalcMetric Value
        {
            set
            {
                Clear();
                if (value != null)
                {
                    infoLabel.Text = value.Id.ToString();
                    nameTextBox.Text = value.Name;

                    // ACTION COMBOBOX
                    actionBindingSource.Position = -1;
                    if (value.ActionId > 0)
                    {
                        Action a = ((Action[])actionBindingSource.DataSource).FirstOrDefault(x => x.Id == value.ActionId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует действие id=" + value.ActionId);
                        actionComboBox.SelectedItem = a;
                    }

                    // fieldIni COMBOBOX
                    fieldIniBindingSource.Position = -1;
                    if (value.FieldIdIni > 0)
                    {
                        Field a = ((List<Field>)fieldIniBindingSource.DataSource).FirstOrDefault(x => x.Id == value.FieldIdIni);
                        if (a == null) throw new Exception("В выпадающем списке исходных полей отсутствует элемент с id=" + value.FieldIdIni);
                        fieldIniComboBox.SelectedItem = a;
                    }

                    // fieldIni COMBOBOX
                    fieldAngBindingSource.Position = -1;
                    if (value.FieldIdIni > 0)
                    {
                        Field a = ((List<Field>)fieldAngBindingSource.DataSource).FirstOrDefault(x => x.Id == value.FieldIdAng);
                        if (a == null) throw new Exception("В выпадающем списке полей аналогов отсутствует элемент с id=" + value.FieldIdAng);
                        fieldAngComboBox.SelectedItem = a;
                    }

                    // Группа FieldTimeShiftWeights 
                    ucTimeShiftWeights.Fill(value.AngFieldTimeShiftWeights);

                    isActualCheckBox.Checked = value.IsActual;
                }
            }
            get
            {
                try
                {
                    return new TaskCalcMetric()
                    {
                        Id = string.IsNullOrEmpty(infoLabel.Text) ? -1 : int.Parse(infoLabel.Text),
                        Name = nameTextBox.Text,
                        FieldIdIni = ((Field)fieldIniComboBox.SelectedItem).Id,
                        FieldIdAng = ((Field)fieldAngComboBox.SelectedItem).Id,
                        AngTimeQBack = (int)timeWindowInterval.GetInterval()[0],
                        AngTimeQForward = (int)timeWindowInterval.GetInterval()[1],
                        ActionId = ((Action)actionComboBox.SelectedItem).Id,
                        ActionClimate = ucClimate.Value,
                        AngFieldTimeShiftWeights = ucTimeShiftWeights.GetIntDoubleList().ToArray(),
                        AngDateS = null,
                        AngDateF = null,
                        IniDateS = null,
                        IniDateF = null,
                        IsActual = isActualCheckBox.Checked
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
            ClearTextBox(this);
            fieldIniBindingSource.Position = -1;
            fieldAngBindingSource.Position = -1;
            actionBindingSource.Position = -1;
        }
        /// <summary>
        /// Проинициализировать все TextBox элемента управления с учётом вложенных.
        /// </summary>
        /// <param name="control">Элемент управления.</param>
        /// <param name="text">Значение для инициализации.</param>
        void ClearTextBox(Control control, string text = null)
        {
            if (typeof(TextBox) == control.GetType())
                ((TextBox)control).Text = text;
            foreach (Control item in control.Controls)
            {
                ClearTextBox(item, text);
            }
        }
        ///// <summary>
        ///// Проинициализировать все ComboBox элемента управления с учётом вложенных.
        ///// </summary>
        ///// <param name="control">Элемент управления.</param>
        ///// <param name="selectedIndex">Значение для инициализации.</param>
        //void ClearComboBox(Control control, int selectedIndex = -1)
        //{
        //    if (typeof(ComboBox) == control.GetType())
        //        ((ComboBox)control).SelectedIndex = selectedIndex;
        //    foreach (Control item in control.Controls)
        //    {
        //        ClearComboBox(item, selectedIndex);
        //    }
        //}
        private void needClimateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ucClimate.Enabled = needClimateCheckBox.Checked;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                TaskCalcMetric value = Value;
                if (value == null)
                {
                    MessageBox.Show("Отсутствует значение для сохранения. Возможно, заполнены не всё обязательные поля формы." +
                        "\nИнформация не сохранена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (value.Id > 0)
                    DataManager.GetInstance().TaskCalcMetricRepository.Update(value);
                else
                    DataManager.GetInstance().TaskCalcMetricRepository.Insert(value);

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
