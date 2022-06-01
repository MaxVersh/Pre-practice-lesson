using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace xml_writer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Button1_Click(object sender, EventArgs e) //Добавление данных в форму
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка.");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text; // Имя
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value; // Возраст
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text; // Программист
            }
        }

        private void Button4_Click(object sender, EventArgs e) //сохранение данных из формы в XML
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пустой кэш данных(до заполнения)
                DataTable dt = new DataTable(); // создаем пустую таблицу данных(до заполнения)
                dt.TableName = "Employee"; // название таблицы
                dt.Columns.Add("Name"); // название колонок отвечающих за имя пользователя
                dt.Columns.Add("Age");// название колонок отвечающих за возраст пользователя
                dt.Columns.Add("Programmer");// название колонок отвечающих за то , является ли пользователь программистом
                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                {
                    DataRow row = ds.Tables["Employee"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                    row["Name"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                    row["Age"] = r.Cells[1].Value; //в столбец этой строки заносим данные из второго столбца dataGridView1
                    row["Programmer"] = r.Cells[2].Value; //в столбец этой строки заносим данные из третьего столбца dataGridView1
                    ds.Tables["Employee"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                }
                ds.WriteXml("C:\\Users\\Public\\Documents\\XmlForFR\\Data.xml");
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }
        }

        private void Button5_Click(object sender, EventArgs e) //загрузка файла XML в форму
        {
            if (dataGridView1.Rows.Count > 0) //если в таблице больше нуля строк
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла.", "Ошибка.");
            }
            else
            {
                if (File.Exists("C:\\Users\\Public\\Documents\\XmlForFR\\Data.xml")) // если существует данный файл
                {
                DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                ds.ReadXml("C:\\Users\\Public\\Documents\\XmlForFR\\Data.xml"); // записываем в него XML-данные из файла
                
                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // добавляем новую сроку в dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // заносим в первый столбец созданной строки данные из первого столбца таблицы ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Age"]; // то же самое со вторым столбцом
                        dataGridView1.Rows[n].Cells[2].Value = item["Programmer"]; // то же самое с третьим столбцом
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Ошибка.");
                }
            }
        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e) // выбор нужной строки для редактирования
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            numericUpDown1.Value = n;
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void Button2_Click(object sender, EventArgs e) //редактирование
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Ошибка.");
            }
        }

        private void Button3_Click(object sender, EventArgs e) //удалить выбранную строку
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка.");
            }
        }

        private void Button6_Click(object sender, EventArgs e) //очистить таблицу
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пустая.", "Ошибка.");
            }
        }

    }
}
