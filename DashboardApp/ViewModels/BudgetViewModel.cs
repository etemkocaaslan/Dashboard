using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DashboardApp.Models;

namespace DashboardApp.ViewModels
{
    public class BudgetViewModel : ViewModelBase
    {
        private ObservableCollection<BudgetItem> _budgetItems = new ObservableCollection<BudgetItem>();
        public ObservableCollection<BudgetItem> BudgetItems
        {
            get => _budgetItems;
            set => SetProperty(ref _budgetItems, value);
        }

        private BudgetItem _selectedBudgetItem;
        public BudgetItem SelectedBudgetItem
        {
            get => _selectedBudgetItem;
            set
            {
                SetProperty(ref _selectedBudgetItem, value);
                EditBudgetItemCommand.NotifyCanExecuteChanged();
                DeleteBudgetItemCommand.NotifyCanExecuteChanged();
            }
        }

        private BudgetItem _newBudgetItem = new BudgetItem();
        public BudgetItem NewBudgetItem
        {
            get => _newBudgetItem;
            set => SetProperty(ref _newBudgetItem, value);
        }

        public IRelayCommand AddBudgetItemCommand { get; }
        public IRelayCommand EditBudgetItemCommand { get; }
        public IRelayCommand DeleteBudgetItemCommand { get; }

        public BudgetViewModel()
        {
            AddBudgetItemCommand = new RelayCommand(AddBudgetItem);
            EditBudgetItemCommand = new RelayCommand(EditBudgetItem, () => SelectedBudgetItem != null);
            DeleteBudgetItemCommand = new RelayCommand(DeleteBudgetItem, () => SelectedBudgetItem != null);
        }

        private void AddBudgetItem()
        {
            if (!string.IsNullOrWhiteSpace(NewBudgetItem.Name) && NewBudgetItem.Amount != 0)
            {
                NewBudgetItem.Id = BudgetItems.Count + 1;
                BudgetItems.Add(new BudgetItem
                {
                    Id = NewBudgetItem.Id,
                    Name = NewBudgetItem.Name,
                    Amount = NewBudgetItem.Amount,
                    Date = NewBudgetItem.Date,
                    IsIncome = NewBudgetItem.IsIncome
                });
                NewBudgetItem = new BudgetItem();
            }
        }

        private void EditBudgetItem()
        {
            if (SelectedBudgetItem != null)
            {
                var item = BudgetItems.FirstOrDefault(b => b.Id == SelectedBudgetItem.Id);
                if (item != null)
                {
                    item.Name = SelectedBudgetItem.Name;
                    item.Amount = SelectedBudgetItem.Amount;
                    item.Date = SelectedBudgetItem.Date;
                    item.IsIncome = SelectedBudgetItem.IsIncome;
                }
            }
        }

        private void DeleteBudgetItem()
        {
            if (SelectedBudgetItem != null)
            {
                BudgetItems.Remove(SelectedBudgetItem);
            }
        }
    }
}
