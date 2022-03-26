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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var spisok = db.Table
                .Where(n => n.Name == textBox1.Text)
                .GroupBy(n => n.Class)
                .Select(n => new
                {
                    Class = n.Key,
                    Count = n.GroupBy(z => z.Surname + " " + z.Initial)
                                                            .Where(z => z.Min(x => x.Rate) != 2)
                                                            .Where(z => z.Sum(x => x.Rate) / z.Count() >= 3.5).Count()
                }).OrderBy(n => n.Class);

            dataGridView1.DataSource = spisok;


        }
    }
}
