using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.SGMO
{
    public partial class UCMessageSiteTable : UserControl
    {
        public UCMessageSiteTable()
        {
            InitializeComponent();

            dgv.Columns.Add("site", "Пункт/объект");
            dgv.Columns.Add("method", "Метод");
            dgv.Columns.Add("messageType", "Тип");
            dgv.Columns.Add("langType", "Язык");
            dgv.Columns.Add("dateIni", "Дата исх.");
            dgv.Columns.Add("dateFcs", "Дата прог.");
            dgv.Columns.Add("leadHours", "Забл (ч)");
            dgv.Columns.Add("dateTypeFcs", "Тип даты прог.");
            dgv.Columns.Add("text", "Текст");
            dgv.Columns.Add("dateInsert", "Дата встав.");
        }

        public void Fill(List<MessageSite> msgs)
        {
            dgv.Rows.Clear();

            if (msgs != null)
            {
                foreach (var msg in msgs)
                {
                    DataGridViewRow row = dgv.Rows[dgv.Rows.Add(new object[]
                    {
                        Amur.Meta.SiteRepository.GetCash().First(x=>x.Id== msg.SiteId).GetName(0,Amur.Meta.SiteTypeRepository.GetCash()),
                        Amur.Meta.MethodRepository.GetCash().First(x=>x.Id== msg.MethodId),
                        SGMO.MessageTypeRepository.GetCash().First(x=>x.Id== (int)msg.MessageType).Name,
                        msg.Language.ToString(),
                        msg.DateIni.ToString(UCMessageSite.DATETIME_FORMAT),
                        msg.DateFcs.ToString(UCMessageSite.DATETIME_FORMAT),
                        (msg.DateFcs - msg.DateIni).TotalHours.ToString(),
                        msg.DateTypeFcs,
                        msg.Text,
                        msg.DateInsert.ToString(UCMessageSite.DATETIME_FORMAT + ":mm")
                })];
                    row.Tag = msg;
                }
            }
            RaiseSelectedItemChanged();
        }

        public MessageSite GetCurrentItem()
        {

            return dgv.CurrentRow == null || dgv.CurrentRow.Tag == null ? null : (MessageSite)dgv.CurrentRow.Tag;
        }

        public delegate void UCSelectedItemChangedEventHandler(MessageSite curItem);
        public event UCSelectedItemChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedItemChanged()
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged(GetCurrentItem());
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RaiseSelectedItemChanged();
        }
    }
}