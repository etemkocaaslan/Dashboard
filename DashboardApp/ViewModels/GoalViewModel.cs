using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DashboardApp.Models;

namespace DashboardApp.ViewModels
{
    public class GoalViewModel : ViewModelBase
    {
        private ObservableCollection<Goal> _goals = new ObservableCollection<Goal>();
        public ObservableCollection<Goal> Goals
        {
            get => _goals;
            set => SetProperty(ref _goals, value);
        }

        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                SetProperty(ref _selectedGoal, value);
                EditGoalCommand.NotifyCanExecuteChanged();
                DeleteGoalCommand.NotifyCanExecuteChanged();
            }
        }

        private Goal _newGoal = new Goal();
        public Goal NewGoal
        {
            get => _newGoal;
            set => SetProperty(ref _newGoal, value);
        }

        public IRelayCommand AddGoalCommand { get; }
        public IRelayCommand EditGoalCommand { get; }
        public IRelayCommand DeleteGoalCommand { get; }

        public GoalViewModel()
        {
            AddGoalCommand = new RelayCommand(AddGoal);
            EditGoalCommand = new RelayCommand(EditGoal, () => SelectedGoal != null);
            DeleteGoalCommand = new RelayCommand(DeleteGoal, () => SelectedGoal != null);
        }

        private void AddGoal()
        {
            if (!string.IsNullOrWhiteSpace(NewGoal.Title))
            {
                NewGoal.Id = Goals.Count + 1;
                Goals.Add(new Goal
                {
                    Id = NewGoal.Id,
                    Title = NewGoal.Title,
                    Description = NewGoal.Description,
                    Deadline = NewGoal.Deadline,
                    IsCompleted = NewGoal.IsCompleted
                });
                NewGoal = new Goal();
            }
        }

        private void EditGoal()
        {
            if (SelectedGoal != null)
            {
                var item = Goals.FirstOrDefault(g => g.Id == SelectedGoal.Id);
                if (item != null)
                {
                    item.Title = SelectedGoal.Title;
                    item.Description = SelectedGoal.Description;
                    item.Deadline = SelectedGoal.Deadline;
                    item.IsCompleted = SelectedGoal.IsCompleted;
                }
            }
        }

        private void DeleteGoal()
        {
            if (SelectedGoal != null)
            {
                Goals.Remove(SelectedGoal);
            }
        }
    }
}
