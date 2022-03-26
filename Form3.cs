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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var spisok = db.Table2
                .GroupJoin(db.Table2_1, n => n.Id, m => m.ID_user, (t, z) => new { count = z.Count(),  id = t.Id, street = t.Street })
                .OrderBy(n => n.count)
                .ThenBy(n => n.id);

            dataGridView1.DataSource = spisok;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var spisok = db.Table2_1
                .Join(db.Table2, n => n.ID_user, m => m.Id, (t, z) => new { name = t.Name, year = z.Year, id = z.Id, disc = t.Discount })
                .GroupBy(n => n.name)
                .Select(n => new { 
                    name = n.Key, 
                    id = n.Where(x => x.disc == n.Max(m => m.disc)).First().id, 
                    year = n.Where(x => x.disc == n.Max(m => m.disc)).First().year, 
                    discount = n.Max(x => x.disc)})
                .OrderBy(n => n.name);

            dataGridView1.DataSource = spisok;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
