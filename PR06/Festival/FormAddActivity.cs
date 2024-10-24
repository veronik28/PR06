using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.Json;
using Festival.DBCon;

namespace Festival
{
    public partial class FormAddActivity : Form
    {
        public FormAddActivity()
        {
            InitializeComponent();
        }

        private List<int> Id_Juri = new List<int>();

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddJuri_Click(object sender, EventArgs e)
        {
            int id = (int)juriComboBox.SelectedValue;
            if (!Id_Juri.Contains(id))
            {
                Id_Juri.Add(id);
                MessageBox.Show("Член добавлен!");
            }
            else
            {
                MessageBox.Show("Член уже добавлен!");
            }
        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Укажите полное название!");
                return;
            }

            Convert.ToInt32(dayTextBox.Text);
            if (Id_Juri.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одного члена жюри!");
                return;
            }

            // Добавление активности
            Activity activity = new Activity
            {
                Title = titleTextBox.Text,
                EventPlanID = (int)eventPlanIDComboBox.SelectedValue,
                StartedAt = dateTimePicker1.Value,
                Day = Convert.ToInt32(dayTextBox.Text),
                // Другие параметры
            };
            DBConst.model.Activity.Add(activity);

            // Сохранение изменений
            DBConst.model.SaveChanges();
        }

        private void FormAddActivity_Load(object sender, EventArgs e)
        {
            eventBindingSource.DataSource = DBConst.model.Event.ToList();
            userBindingSource.DataSource = DBConst.model.User.Where(x => x.RoleID != 1).ToList();
            userBindingSource.DataSource = DBConst.model.User.Where(x => x.RoleID != 1).ToList();
        }
    }
}
