using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            

            var spisok = db.Table3_2
                .Join(db.Table2, n => n.ID_Buyer, m => m.Id, (t, z) => new { idBuy = z.Id, street = z.Street, articul = t.Articul, name = t.Name })
                .Join(db.Table3_1, n => n.articul, m => m.Articul, (t, z) => new { idBuy = t.idBuy, street = t.street, country = z.Country, name = t.name })
                .Join(db.Table2_1, n => n.name, m => m.Name, (t, z) => new { idBuy = t.idBuy, street = t.street, country = t.country, disc = z.Discount, idMag = z.ID_user })
                .GroupBy(n => n.country+"-"+n.street)
                .Select(n => new { country = n.Key.Substring(0, n.Key.IndexOf("-")), 
                    street = n.Key.Substring(n.Key.IndexOf("-")+1, n.Key.Length - n.Key.IndexOf("-")-1),
                    discount = (n.Where(x => x.idMag == x.idBuy).Select(r => r.disc).Max().Value==null?0: n.Where(x => x.idMag == x.idBuy).Select(r => r.disc).Max().Value)})
                .OrderBy(n => n.country)
                .ThenBy(n => n.street);

            dataGridView1.DataSource = spisok;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
