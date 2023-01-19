using CommunityToolkit.Maui.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpoqZeroTaDA.Custom
{
    public class AnimatedTapGesture : BaseBehavior<View>
    {




        View view;
        public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimatedTapGesture));



        public static readonly BindableProperty CommandParameterProperty =
         BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AnimatedTapGesture));



        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }



        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }




        public static readonly BindableProperty ScaleProperty =
        BindableProperty.Create(nameof(Command), typeof(double), typeof(AnimatedTapGesture), 0.95);



        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(CommandProperty, value);
        }




        TapGestureRecognizer tapGesture;
        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            tapGesture = new TapGestureRecognizer();
            bindable.GestureRecognizers.Add(tapGesture);
            tapGesture.Tapped += TapGesture_Tapped;
            view = bindable;
        }



        private async void TapGesture_Tapped(object sender, TappedEventArgs e)
        {
            await view?.ScaleTo(Scale, 125);
            await view?.ScaleTo(1, 125);
            var d = Command;
            var ddwsds = CommandProperty;
            var ddwsdfds = CommandParameter;
            if (Command?.CanExecute(CommandParameter) ?? false)
            {
                Command.Execute(CommandParameter);
            }
        }



        protected override void OnDetachingFrom(View bindable)
        {
            tapGesture.Tapped -= TapGesture_Tapped;
            tapGesture = null;
            view = null;
        }




    }
}
