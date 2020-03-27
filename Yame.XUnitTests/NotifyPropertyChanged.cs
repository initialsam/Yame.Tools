using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Abstractions;

namespace Yame.XUnitTests
{
    public class NotifyPropertyChanged
    {
        private readonly ITestOutputHelper _output;

        public NotifyPropertyChanged(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
        }
        [Fact]
        public void NotifyWhenNameChanged()
        {
            Person sut = new Person();
            sut.PropertyChanged += OnPropertyChanged;
            Assert.PropertyChanged(sut, "Name", () => sut.Name = "Amrit");
            //Property Name was changed.
        }


        [Fact]
        public void NotifyWhenAgeChanged()
        {
            Person sut = new Person();
            sut.PropertyChanged += OnPropertyChanged;
            Assert.PropertyChanged(sut, "Age", () => sut.Age = 42);
            //Property Age was changed.
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _output.WriteLine($"Property {e.PropertyName} was changed.");
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private int _age;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }


        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
