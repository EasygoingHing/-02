using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AccidentGraph
{
    public partial class MainWindow : Window
    {
        private const int NumberOfMonths = 12;//число месяцев
        private int MinNumberOfAccidents = 10;//минимальное число аварий
        private int MaxNumberOfAccidents = 30;//максимальное число аварий
        private const int WinterMonthStartIndex = 11;//начало зимнего месяца
        private const int WinterMonthEndIndex = 1;//конец зимнего
        private const int RefreshIntervalInSeconds = 2;//обновление интервала
        private int additionalAccidents;//добавление аварий к ьекущим
        private int[] accidentData = new int[NumberOfMonths]; //массив с авариями
        private int currentMonthIndex = 0; //индекс текущего месяца
        private Random random = new Random();
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(RefreshIntervalInSeconds);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {                      
            if (int.TryParse(tbMax.Text, out int max) && int.TryParse(tbMin.Text, out int min))
            {
                if (max > min && min >= 20 && max <= 1000)
                {
                    MaxNumberOfAccidents = max; MinNumberOfAccidents = min;
                }
                else
                {
                    MessageBox.Show("Несоответствие условию");
                }                
            }
            else
            {
                tbMin.Focus();
                tbMax.Clear();
                tbMin.Clear();
                MessageBox.Show("Отсутствуют данные или неверный формат данных");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateAccidentData();
            DrawGraph();
            DisplayData();
            currentMonthIndex = (currentMonthIndex + 1) % NumberOfMonths;
        }       

        private void UpdateAccidentData()//обновление графика
        {
            accidentData[currentMonthIndex] = random.Next(MinNumberOfAccidents, MaxNumberOfAccidents + 1);

            if (currentMonthIndex <= WinterMonthEndIndex || currentMonthIndex >= WinterMonthStartIndex)//если зимние месяца + к авариям
            {
                additionalAccidents = random.Next(50, 60);                
                accidentData[currentMonthIndex] += additionalAccidents;                
            }            
                       
        }

        private void DrawGraph()//отображение графика
        {
            canvas.Children.Clear();

            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;
            double barWidth = canvasWidth / NumberOfMonths;
            var months = new string[NumberOfMonths]
            { "Jan", "Feb", "Mar", "Apr",
              "May", "Jun", "Jul", "Aug",
              "Sep","Oct", "Nov", "Dec"};

            for (int i = 0; i < NumberOfMonths; i++)
            {
                double barHeight = accidentData[i] * canvasHeight / (MaxNumberOfAccidents + additionalAccidents);                

                Rectangle rect = new Rectangle();
                rect.Width = barWidth;
                rect.Height = barHeight;
                rect.Fill = Brushes.Blue;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 0.5;

                var monthLabel = new TextBlock()
                {
                    Text = months[i],
                    Foreground = Brushes.Black,
                    FontSize = 14
                };

                var accidentDataLabel = new TextBlock()
                {
                    Text = accidentData[i].ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 14
                };

                var nameMonthLabel = new TextBlock()
                {
                    Text = "Months ",
                    Foreground = Brushes.Black,
                    FontSize = 14
                };

                var numberAccidents = new TextBlock()
                {
                    Text = "NumberAccidents ",
                    Foreground = Brushes.Black,
                    FontSize = 14,
                };

                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 10);
                canvas.Children.Add(rect);

                Canvas.SetLeft(monthLabel, i * barWidth + 18);
                Canvas.SetBottom(monthLabel, -10);
                canvas.Children.Add(monthLabel);

                Canvas.SetLeft(accidentDataLabel, i * barWidth + 22);
                Canvas.SetBottom(accidentDataLabel, barHeight + 10);
                canvas.Children.Add(accidentDataLabel);

                Canvas.SetLeft(nameMonthLabel, -90);
                Canvas.SetBottom(nameMonthLabel, -8);
                canvas.Children.Add(nameMonthLabel);

                Canvas.SetLeft(numberAccidents, -115);
                Canvas.SetBottom(numberAccidents, 298);
                canvas.Children.Add(numberAccidents);
            }
        }

        private void DisplayData()//вывод информации
        {
            int currentData = accidentData[currentMonthIndex];
            currentDataTextBlock.Text = "Текущие показания: " + currentData;

            int sum = 0;
            for (int i = 0; i < NumberOfMonths; i++)
            {
                sum += accidentData[i];
            }

            int averageData = sum / NumberOfMonths;
            forecastDataTextBlock.Text = "Средние прогнозируемые показания: " + averageData;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Autorization autoriz = new Autorization();
            autoriz.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataAutorization.Login == "Admin" && DataAutorization.Password == "Admin")
            {                
                btnInput.Visibility = Visibility.Visible;
                tbMax.Visibility = Visibility.Visible;
                tbMin.Visibility = Visibility.Visible;
                lbMax.Visibility = Visibility.Visible;
                lbMin.Visibility = Visibility.Visible;
            }
            else
            {
                btnInput.Visibility = Visibility.Hidden;
                tbMax.Visibility = Visibility.Hidden;
                tbMin.Visibility = Visibility.Hidden;
                lbMax.Visibility = Visibility.Hidden;
                lbMin.Visibility = Visibility.Hidden;
            }
        }
    }
}
