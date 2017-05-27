using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTaskManager.Models;

namespace TodoTaskManager
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields 
        private List<TaskItem> tasks;
        private ObservableCollection<TaskItem> taskCollection;
        private ObservableCollection<TaskItem> completedTasksCollection;
        private TaskItem selectedTask;
        private string newTaskName;
        private bool showNameEditor;
        private bool showCompletedTasks;

        private DelegateCommand addNewTaskCommand;
        private DelegateCommand removeTaskCommand;
        private DelegateCommand enterCommand;
        private DelegateCommand completedTasksCommand;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            InitTaskList();
        }
        #endregion

        #region Properties
        public ObservableCollection<TaskItem> TaskCollection
        {
            get { return this.taskCollection ?? (this.taskCollection = new ObservableCollection<TaskItem>()); }
        }

        public ObservableCollection<TaskItem> CompletedTasksCollection
        {
            get { return this.completedTasksCollection ?? (this.completedTasksCollection = new ObservableCollection<TaskItem>()); }
        }

        public TaskItem SelectedTask
        {
            get { return this.selectedTask; }
            set { this.SetProperty(ref this.selectedTask, value); }
        }

        public DelegateCommand AddNewTaskCommand
        {
            get { return this.addNewTaskCommand ?? (this.addNewTaskCommand = new DelegateCommand(OnAddNewTask, CanAddNewTask)); }
        }

        public DelegateCommand RemoveTaskCommand
        {
            get { return this.removeTaskCommand ?? (this.removeTaskCommand = new DelegateCommand(OnRemoveTask)); }
        }

        public DelegateCommand EnterCommand
        {
            get { return this.enterCommand ?? (this.enterCommand = new DelegateCommand(OnEnter)); }
        }

        public DelegateCommand CompletedTasksCommand
        {
            get { return this.completedTasksCommand ?? (this.completedTasksCommand = new DelegateCommand(OnShowCompletedTasks)); }
        }

        public string NewTaskName
        {
            get { return this.newTaskName; }
            set { this.SetProperty(ref this.newTaskName, value); }
        }

        public bool ShowNameEditor
        {
            get { return this.showNameEditor; }
            set { this.SetProperty(ref this.showNameEditor, value); }
        }

        public bool ShowCompletedTasks
        {
            get { return this.showCompletedTasks; }
            set { this.SetProperty(ref this.showCompletedTasks, value); }
        }
        #endregion

        #region Methods
        private void InitTaskList()
        {
            tasks = new List<TaskItem>
            {
                new TaskItem {TaskName = "Get Groceries", IsComplete = false },
                new TaskItem {TaskName = "Get Mother's Day Gift", IsComplete = false },
                new TaskItem {TaskName = "Wash Car", IsComplete = true },
                new TaskItem {TaskName = "Clean Bathroom", IsComplete = false },
                new TaskItem {TaskName = "Go to Friends Birthday party", IsComplete = false }
            };

            this.TaskCollection.Clear();
            foreach (var item in tasks)
            {
                this.TaskCollection.Add(item);
            }
        }


        private bool CanAddNewTask()
        {
            if (this.TaskCollection.Count == 0) return false;
            return true;   
        }

        private void OnAddNewTask()
        {
            this.ShowNameEditor = true;
        }

        private void OnEnter()
        {
            TaskItem newTask = new TaskItem
            {
                TaskName = this.NewTaskName,
                IsComplete = false
            };
            this.TaskCollection.Add(newTask);
            this.ShowNameEditor = false;
        }

        private void OnRemoveTask()
        {
            if (this.SelectedTask != null)
            {
                this.TaskCollection.Remove(this.SelectedTask);
            }
        }

        private void OnShowCompletedTasks()
        {
            //this.ShowCompletedTasks = true;
            var completedTasks = this.TaskCollection.Where(t => t.IsComplete).ToList();

            this.CompletedTasksCollection.Clear();
            foreach (var item in completedTasks)
            {
                this.CompletedTasksCollection.Add(item);
            }
        }
        #endregion
    }
}
