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
        public void SetDictionaryes(List<Action> actions, List<MetaDb.DbInterface> dbInterfaces, List<MetaDb.Db> dbLists, List<Amur.Meta.MathVar> mathvars)
        {
            ucField.SetListActions(actions);
            ucField.SetListDbLists(dbInterfaces, dbLists);
        }

        void Clear()
        {
            fieldBindingSource.Clear();
        }
        List<int> _fieldIds = new List<int>();
        public void Fill(List<int> fieldIds = null)
        {
            Clear();
            _fieldIds = fieldIds;

            List<Field> fields = DataManager.GetInstance().FieldRepository.Select(fieldIds);
            fieldBindingSource.DataSource = fields.OrderBy(x => x.Name);
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
                ActionId = (int)Enums.Action.NoAction,
                CatalogDbInterfaceId = (int)MetaDb.Enums.DbInterface.Sakura_Catalog,
                FieldTimeShiftWeights = new IntDouble[] { new IntDouble() { Int = 0, Double = 1 } }
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
            ucFieldGeoobList.Clear();
            ucField.Clear();
            if (CurrentField != null)
            {
                ucField.Value = CurrentField;
                ucFieldGeoobList.Fill4Field(CurrentField.Id);
            }
        }

        private void ucField_UCItemSaved()
        {
            Fill(_fieldIds);
        }

        private void cloneCurrentButton_Click(object sender, EventArgs e)
        {
            try
            {
                Field curField = CurrentField;
                if (curField != null)
                {
                    string catalogId = Common.FormEnterString.Show("Введите код записи каталога для поля");
                    Field newField = new Field()
                    {
                        Id = -1,
                        Name = "#" + curField.Name,
                        CatalogId = int.Parse(catalogId),
                        CatalogDbInterfaceId = curField.CatalogDbInterfaceId,

                        ActionId = curField.ActionId,
                        Climate = curField.Climate,
                        YearS = curField.YearS,
                        TimeQBack = curField.TimeQBack,
                        TimeQForward = curField.TimeQForward,
                        FieldTimeShiftWeights = curField.FieldTimeShiftWeights
                    };
                    newField.Id = DataManager.GetInstance().FieldRepository.Insert(newField);

                    List<_DELME_FieldGeoobs> fieldGeos = DataManager.GetInstance().FieldGeoobsRepository.SelectByField(curField.Id);
                    fieldGeos.ForEach(x => x.Id = -1);
                    fieldGeos.ForEach(x => x.FieldId = newField.Id);
                    foreach (var item in fieldGeos)
                    {
                        item.Id = DataManager.GetInstance().FieldGeoobsRepository.Insert(item);
                    }

                    if (_fieldIds != null) _fieldIds.Add(newField.Id);
                    Fill(_fieldIds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
