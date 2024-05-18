using DashboardApp.ViewModels;
using System.Windows.Controls;

namespace DashboardApp.Views
{
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            DataContext = new CalendarViewModel();
        }
    }
}
