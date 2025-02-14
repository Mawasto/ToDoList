using ToDoList1.ViewModels;

namespace ToDoList1.Views;

public partial class ToDoPage : ContentPage
{
    public ToDoPage()
    {
        InitializeComponent();
        BindingContext = new TodoViewModel();
    }
}
