using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardApp.Models;
using System.Collections.ObjectModel;

namespace DashboardApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        public MainViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            AddTodoCommand = new RelayCommand(AddTodo);
        }

        public IRelayCommand AddTodoCommand { get; }

        private void AddTodo()
        {
            TodoItems.Add(new TodoItem { Id = TodoItems.Count + 1, Title = "New Task", Description = "Description", DueDate = DateTime.Now });
        }
    }
}
