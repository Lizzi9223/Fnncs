using Finances.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.ViewModel
{
    public class MainControlVM : BaseVM
    {
        public ObservableCollection<Income> Incomes { get; set; }
        public ObservableCollection<Cost> Costs { get; set; }
        public ObservableCollection<Cost> LastCosts { get; set; }
        private double allIncome;
        public double AllIncome
        {
            get
            {
                return allIncome;
            }
            set
            {
                allIncome = value;
                OnPropertyChanged("AllIncome");
            }
        }
        private double allCost;
        public double AllCost
        {
            get
            {
                return allCost;
            }
            set
            {
                allCost = value;
                OnPropertyChanged("AllCost");
            }
        }
        private double maxCost;
        public double MaxCost
        {
            get
            {
                return maxCost;
            }
            set
            {
                maxCost = value;
                OnPropertyChanged("MaxCost");
            }
        }
        private double maxIncome;
        public double MaxIncome
        {
            get
            {
                return maxIncome;
            }
            set
            {
                maxIncome = value;
                OnPropertyChanged("MaxIncome");
            }
        }
        
        private UnitOfWork unit;
        private List<Cost> MonthlyCosts;
        private List<Income> MonthlyIncomes;
        public SeriesCollection ChartSeries { get; set; }
        public MainControlVM()
        {
            unit = new UnitOfWork();
            MonthlyCosts = unit.Users.GetMonthlyCosts();
            MonthlyIncomes = unit.Users.GetMonthlyIncomes();
            var group = MonthlyCosts.GroupBy(c => c.CategoryName);
            LastCosts = unit.Users.GetCosts();
            LastCosts = new ObservableCollection<Cost>(LastCosts.OrderByDescending(c => c.date_when));
            if(LastCosts.Count > 4)
            {
                LastCosts = new ObservableCollection<Cost> { LastCosts[0], LastCosts[1], LastCosts[2], LastCosts[3] };
            }
            Incomes = unit.Users.GetIncomes();
            Costs = unit.Users.GetCosts();
            AllIncome = Incomes.Sum(c => c.sum);
            AllCost = Costs.Sum(c => c.sum);
            if(MonthlyCosts.Count > 0)
                MaxCost = MonthlyCosts.Max(c => c.sum);
            if(MonthlyIncomes.Count > 0)
                MaxIncome = MonthlyIncomes.Max(c => c.sum);

            ChartSeries = new SeriesCollection();
            foreach(IGrouping<string, Cost> item in group)
            {
                ChartSeries.Add(new PieSeries { Title = item.Key, Values = new ChartValues<double> { item.Sum(c => c.sum) } });
            }
        }
        
    }


    public class ChartData
    {
        private List<PieSeries> pies;

        public List<PieSeries> Pies
        {
            get { return pies; }
            set { pies = value; }
        }

        public ChartData()
        {
            Pies = new List<PieSeries>();
        }
        public void AddAPie(string category, double sum)
        {
            pies.Add(new PieSeries { Title = category, Values = new ChartValues<double> {sum } });
        }
    }
}
