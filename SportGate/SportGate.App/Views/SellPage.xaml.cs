using SportGate.App.ViewModels;

namespace SportGate.App.Views;

public partial class SellPage : ContentPage
{
    private readonly SellViewModel _vm;

    public SellPage(SellViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
        Loaded += SellPage_Loaded;
    }

    private async void SellPage_Loaded(object sender, EventArgs e)
    {
        await _vm.InitializeAsync();
        CreatePeopleControls();
    }

    private void CreatePeopleControls()
    {
        PeopleContainer.Children.Clear();
        foreach (var cat in _vm.Categories)
        {
            var grid = new Grid { ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition(GridLength.Star), new ColumnDefinition(80) } };
            var lbl = new Label { Text = $"{cat.Description} ({cat.Code}) - {cat.Price:C}" };
            var step = new Stepper { Minimum = 0, Maximum = 20, Increment = 1, HorizontalOptions = LayoutOptions.End };
            var qty = new Label { Text = "0", HorizontalOptions = LayoutOptions.End };

            step.ValueChanged += (s, e) =>
            {
                int val = (int)e.NewValue;
                qty.Text = val.ToString();
                _vm.SetCategoryCount(cat.Id, val);
            };

            grid.Add(lbl, 0, 0);
            var stack = new StackLayout { Orientation = StackOrientation.Horizontal };
            stack.Children.Add(qty);
            stack.Children.Add(step);
            grid.Add(stack, 1, 0);
            PeopleContainer.Children.Add(grid);
        }
    }
}