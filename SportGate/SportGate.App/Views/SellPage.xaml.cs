using SportGate.App.ViewModels;
using System.ComponentModel;

namespace SportGate.App.Views;

public partial class SellPage : ContentPage
{
    private readonly SellViewModel _vm;

    public SellPage(SellViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;

        // 👇 AQUÍ EXACTAMENTE
        _vm.PropertyChanged += Vm_PropertyChanged;

        Loaded += SellPage_Loaded;
    }

    private async void SellPage_Loaded(object sender, EventArgs e)
    {
        await _vm.InitializeAsync();
        CreatePeopleControls();
    }

    private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SellViewModel.SelectedType))
        {
            CreatePeopleControls();
        }
    }

    private void CreatePeopleControls()
    {
        PeopleContainer.Children.Clear();

        if (_vm.SelectedType == null)
            return;

        bool allowMultiple = _vm.SelectedType.AllowMultiplePeople;

        foreach (var cat in _vm.Categories)
        {
            var grid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(200)
            }
            };

            var lbl = new Label
            {
                Text = $"{cat.Description} - {cat.Price:C}",
                VerticalOptions = LayoutOptions.Center
            };

            var qtyLabel = new Label
            {
                Text = "0",
                WidthRequest = 30,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.Center
            };

            var stepper = new Stepper
            {
                Minimum = 0,
                Maximum = allowMultiple ? 20 : 1,
                Increment = 1,
                IsEnabled = true
            };

            stepper.ValueChanged += (_, e) =>
            {
                int val = (int)e.NewValue;

                // 🔒 Si NO permite múltiples, fuerza solo 1 persona total
                if (!allowMultiple && val > 1)
                {
                    stepper.Value = 1;
                    val = 1;
                }

                qtyLabel.Text = val.ToString();

                // 🔥 Si no permite múltiples, limpia los demás
                if (!allowMultiple)
                {
                    foreach (var child in PeopleContainer.Children)
                    {
                        if (child is Grid g && g != grid)
                        {
                            if (g.Children[1] is HorizontalStackLayout st &&
                                st.Children[1] is Stepper otherStepper)
                            {
                                otherStepper.Value = 0;
                            }
                        }
                    }
                }

                _vm.SetCategoryCount(cat.Id, val);
            };

            var stack = new HorizontalStackLayout
            {
                Spacing = 6,
                Children = { qtyLabel, stepper }
            };

            grid.Add(lbl, 0, 0);
            grid.Add(stack, 1, 0);

            PeopleContainer.Children.Add(grid);
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Si ya hubo un ticket creado, reseteamos la venta
        if (_vm.LastCreatedTicket != null)
        {
            _vm.Reset();
            CreatePeopleControls();
        }
    }
}
