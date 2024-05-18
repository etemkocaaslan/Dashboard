using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DashboardApp.Models;

namespace DashboardApp.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        private TodoItem _selectedTodoItem;
        public TodoItem SelectedTodoItem
        {
            get => _selectedTodoItem;
            set
            {
                SetProperty(ref _selectedTodoItem, value);
                EditTodoCommand.NotifyCanExecuteChanged();
                DeleteTodoCommand.NotifyCanExecuteChanged();
            }
        }

        private string _newTodoTitle = string.Empty;
        public string NewTodoTitle
        {
            get => _newTodoTitle;
            set => SetProperty(ref _newTodoTitle, value);
        }

        public IRelayCommand AddTodoCommand { get; }
        public IRelayCommand EditTodoCommand { get; }
        public IRelayCommand DeleteTodoCommand { get; }

        public TodoViewModel()
        {
            AddTodoCommand = new RelayCommand(AddTodo);
            EditTodoCommand = new RelayCommand(EditTodo, () => SelectedTodoItem != null);
            DeleteTodoCommand = new RelayCommand(DeleteTodo, () => SelectedTodoItem != null);
        }

        private void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(NewTodoTitle))
            {
                var newTodo = new TodoItem
                {
                    Id = TodoItems.Count + 1,
                    Title = NewTodoTitle,
                    Description = "",
                    DueDate = DateTime.Now,
                    IsCompleted = false
                };

                TodoItems.Add(newTodo);
                NewTodoTitle = string.Empty;
            }
        }

        private void EditTodo()
        {
            if (SelectedTodoItem != null)
            {
                var item = TodoItems.FirstOrDefault(t => t.Id == SelectedTodoItem.Id);
                if (item != null)
                {
                    item.Title = SelectedTodoItem.Title;
                    item.Description = SelectedTodoItem.Description;
                    item.DueDate = SelectedTodoItem.DueDate;
                    item.IsCompleted = SelectedTodoItem.IsCompleted;
                }
            }
        }

        private void DeleteTodo()
        {
            if (SelectedTodoItem != null)
            {
                TodoItems.Remove(SelectedTodoItem);
            }
        }
    }
}
