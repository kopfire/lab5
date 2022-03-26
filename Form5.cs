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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();


            var spisok = db.Table5_1
                .GroupBy(n => n.Name + "-" + n.Articul)
                .Select(n => new
                {
                    Name = n.Key.Substring(0, n.Key.IndexOf("-")),
                    Articul = n.Key.Substring(n.Key.IndexOf("-") + 1, n.Key.Length - n.Key.IndexOf("-") - 1),
                    Sum = n.Select(y=>y.Price).FirstOrDefault() * (db.Table5_2
                                    .GroupBy(u => u.Name + "-" + u.Articul)
                                    .Select(i => new { t = i.Key, y = i.Count() })
                                    .Where(u => u.t == n.Key)
                                    .FirstOrDefault() == null?0: db.Table5_2
                                    .GroupBy(u => u.Name + "-" + u.Articul)
                                    .Select(i => new { t = i.Key, y = i.Count() })
                                    .Where(u => u.t == n.Key)
                                    .FirstOrDefault().y)
                })
                .OrderBy(n => n.Name)
                .ThenBy(n => n.Articul);

            dataGridView1.DataSource = spisok;
        }
    }
}
