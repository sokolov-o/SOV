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
    public partial class UCFieldList : UserControl
    {
        public UCFieldList()
        {
            InitializeComponent();
        }
        public UCFieldList(List<Action> actions)
        {
            InitializeComponent();
        }
        public void SetDictionaryes(List<Action> actions, List<MetaDb.Db> dbsIMeta)
        {
            ucField.SetDbIMetaList(dbsIMeta);
        }

        void Clear()
        {
            fieldBindingSource.Clear();
        }
        List<int> _fieldIds = new List<int>();
        public void Fill(List<int> fieldIds = null, int? selectedFieldId = null)
        {
            Clear();
            _fieldIds = fieldIds;

            List<Field> fields = DataManager.GetInstance().FieldRepository.Select(fieldIds);
            fieldBindingSource.DataSource = fields.OrderBy(x => x.Name).ToList();

            if (selectedFieldId.HasValue)
                fieldBindingSource.Position = //fieldBindingSource.Find("Id", selectedFieldId);
            ((List<Field>)fieldBindingSource.DataSource).IndexOf(((List<Field>)fieldBindingSource.DataSource).Find(x => x.Id == selectedFieldId));
        }

        private void UCFieldList_Load(object sender, EventArgs e)
        {
            // CREATE COLUMNS
            DataGridViewColumn col;
            string dataPropName;

            dataPropName = "Id";
            col = dgv.Columns[dgv.Columns.Add(dataPropName, "ID")];
            col.DataPropertyName = dataPropName;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            ucField.Value = new Field()
            {
                Id = -1,
                CatalogDbInterfaceId = (int)MetaDb.Enums.DbInterface.Sakura_Catalog,
            };
        }
        public Field CurrentField
        {
            get
            {
                if (fieldBindingSource.Current != null)
                {
                    return ((Field)fieldBindingSource.Current);
                }
                return null;
            }
        }
        private void fieldBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            ucField.Clear();
            if (CurrentField != null)
            {
                ucField.Value = CurrentField;
            }
        }

        private void ucField_UCItemSaved()
        {
            Fill(_fieldIds);
        }
        /// <summary>
        /// Clone current field.
        /// </summary>
        /// <param name="e"></param>
        private void cloneCurrentButton_Click(object sender, EventArgs e)
        {
            try
            {
                Field curField = CurrentField;
                if (curField != null)
                {
                    // CLONE FIELD
                    string catalogId = Common.FormEnterString.Show("Введите код записи каталога для поля");
                    Field newField = new Field()
                    {
                        Id = -1,
                        Name = "#" + curField.Name,
                        CatalogId = int.Parse(catalogId),
                        CatalogDbInterfaceId = curField.CatalogDbInterfaceId
                    };
                    newField.Id = DataManager.GetInstance().FieldRepository.Insert(newField);

                    // CLONE TASK CALC METRIC
                    foreach (var task in DataManager.GetInstance().TaskCalcMetricRepository.SelectByFields(new List<int>() { curField.Id }, new List<int>() { curField.Id }))
                    {
                        task.Id = -1;
                        if (task.FieldIdIni == curField.Id) task.FieldIdIni = newField.Id;
                        if (task.FieldIdAng == curField.Id) task.FieldIdAng = newField.Id;

                        task.Id = DataManager.GetInstance().TaskCalcMetricRepository.Insert(task);
                    }

                    // REFRESH
                    if (_fieldIds != null) _fieldIds.Add(newField.Id);
                    Fill(_fieldIds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void saveFieldButton_Click(object sender, EventArgs e)
        {
            Field field = ucField.Value;
            if (field != null)
            {
                if (field.Id > 0)
                    DataManager.GetInstance().FieldRepository.Update(field);
                else
                    field.Id = DataManager.GetInstance().FieldRepository.Insert(field);
                Fill(null, field.Id);
            }
        }
    }
}
