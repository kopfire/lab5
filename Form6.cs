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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();            


            var spisok = db.Table5_2
                .Join(db.Table6, n => n.Articul, m => m.Articul, (t, z) => new { name = t.Name, articul = t.Articul, country = z.Country})
                .Join(db.Table5_1, 
                    a => new { Id = a.articul, Name = a.name }, 
                    b => new { Id = b.Articul, Name = b.Name },
                    (t, z) => new { name = t.name, articul = t.articul, country = t.country, z.Price })
                .GroupBy(n => n.name + "-" + n.country)
                .Select(n => new
                {
                    Name = n.Key.Substring(0, n.Key.IndexOf("-")),
                    Country = n.Key.Substring(n.Key.IndexOf("-") + 1, n.Key.Length - n.Key.IndexOf("-") - 1),
                    Sum = n.Sum(m => m.Price)
                })
                .OrderBy(n => n.Name)
                .ThenBy(n => n.Country);

            dataGridView1.DataSource = spisok;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();


            var spisok = db.Table6
                .Join(db.Table5_2, n => n.Articul, m => m.Articul, (t, z) => new { country = t.Country, name  = z.Name, articul = z.Articul })
                .Join(db.Table5_1,
                    a => new { Id = a.articul, Name = a.name },
                    b => new { Id = b.Articul, Name = b.Name },
                    (t, z) => new { country = t.country, name = z.Name, articul = z.Articul, price = z.Price})
                .GroupBy(n => n.country)
                .Select(n => new { Country = n.Key, Count = n.Count(), Sum = n.Sum(x => x.price) })
                .OrderBy(n => n.Country);

            dataGridView1.DataSource = spisok;
        }
    }
}
