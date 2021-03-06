using System;
using System.Collections.Generic;
using CovidTracking.API.Models.Entities;
using Moq;
using Tests.Mocks;
using Xunit;

namespace Tests.Countries
{
    public class CountriesServiceTest : MyMocks
    {
        private static Country GenerateValidCountry(string name, double activeCases = 0)
        {
            return new Country(name, activeCases, "", "", "", "", "", "");
        }

        [Fact]
        public void Create_method_country_not_found_error()
        {
            // Dado / Setup
            consumer.Setup((x => x
                .GetByName(It.IsAny<string>())))
                .Throws(new Exception("Country doesn't exists.")
            );

            // Quando / Ação
            Action act = () => countriesService.Create(It.IsAny<string>());

            // Deve / Asserções
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Country doesn't exists.", exception.Message);
            
            repository.Verify(x => x.Add(It.IsAny<Country>()), Times.Never());
        }

        [Fact]
        public void Create_method_no_repeated_contries_error()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName("Brazil"))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x
                .Add(It.IsAny<Country>()))
                .Throws(
                    new Exception("Exception message" ,
                    new Exception("Duplicate entry 'Japan' for key 'IX_Countries_CountryName'"))
            );
            
            // Quando / Ação
            Action act = () => countriesService.Create("Japan");

            // Deve / Asserções
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Duplicate entry 'Japan' for key 'IX_Countries_CountryName'", exception.InnerException.Message);
            
            repository.Verify(x => x.Add(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void Create_method_is_valid()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName("Brazil"))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Add(It.IsAny<Country>()));
            
            // Quando / Ação
            countriesService.Create("Brazil");

            // Deve / Asserções
            repository.Verify(x => x.Add(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void GetCountryByName_method_is_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.Get(It.IsAny<Func<Country, bool>>())).Returns(GenerateValidCountry("Brazil"));
            
            // Quando / Ação
            var response = countriesService.GetCountry("Brazil");

            // Deve / Asserções
            Assert.NotNull(response);
            repository.Verify(x => x.Get(It.IsAny<Func<Country, bool>>()), Times.Once());
        }

        [Fact]
        public void GetCountryByName_method_returns_null()
        {
            // Dado / Setup
            repository.Setup(x => x.Get(It.IsAny<Func<Country, bool>>())).Returns(It.IsAny<Country>());
            
            // Quando / Ação
            var response = countriesService.GetCountry("Brazil");

            // Deve / Asserções
            Assert.Null(response);
            repository.Verify(x => x.Get(It.IsAny<Func<Country, bool>>()), Times.Once());
        }

        [Fact]
        public void Delete_method_is_not_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.Delete(It.IsAny<Country>())).Throws(new Exception("Value cannot be null. (Parameter 'entity')"));
            
            // Quando / Ação
            Action act = () => countriesService.Delete(It.IsAny<string>());

            // Deve / Asserções
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Value cannot be null. (Parameter 'entity')", exception.Message);
            
            repository.Verify(x => x.Delete(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void Delete_method_is_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.Delete(GenerateValidCountry("Brazil", 0)));
            
            // Quando / Ação
            countriesService.Delete("Brazil");

            // Deve / Asserções
            repository.Verify(x => x.Delete(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void Update_method_country_not_founded_error()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Throws(new Exception("Country doesn't exists."));
            
            // Quando / Ação
            Action act = () => countriesService.Update("Brazil");

            // Deve / Asserções
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Country doesn't exists.", exception.Message);

            repository.Verify(x => x.Update(It.IsAny<Country>(), It.IsAny<Country>()), Times.Never());
        }

        [Fact]
        public void Update_method_throws_exception()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Get(It.IsAny<Func<Country, bool>>())).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Update(It.IsAny<Country>(), It.IsAny<Country>())).Throws<Exception>();
            
            // Quando / Ação
            
            Action act = () => countriesService.Update("Brazil");

            // Deve / Asserções
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Update_method_is_valid()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Get(It.IsAny<Func<Country, bool>>())).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Update(It.IsAny<Country>(), It.IsAny<Country>()));
            
            // Quando / Ação
            countriesService.Update("Brazil");

            // Deve / Asserções
            repository.Verify(x => x.Update(It.IsAny<Country>(), It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void GetAll_is_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.GetAll()).Returns(new List<Country>{GenerateValidCountry("Brazil")});
            
            // Quando / Ação
            var response = countriesService.GetAll();

            // Deve / Asserções
            Assert.NotNull(response);
        }

        [Fact]
        public void GetAll_is_not_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.GetAll());
            
            // Quando / Ação
            var response = countriesService.GetAll();

            // Deve / Asserções
            Assert.Null(response);
        }

        public static TheoryData<(List<Country>, List<string>)> PercentageDiference()
        {
            return new TheoryData<(List<Country>, List<string>)>
            {
                (new List<Country>
                {
                    GenerateValidCountry("Brazil", 100000),
                    GenerateValidCountry("China", 10000)
                },
                new List<string>{"Percentual da Diferença em casos ativos entre: Brazil e China = 900,00%"}),
                
                (new List<Country>
                {
                    GenerateValidCountry("Brazil", 6846411),
                    GenerateValidCountry("Spain", 3513213),
                    GenerateValidCountry("USA", 651351),
                    GenerateValidCountry("Japan", 15631),
                    GenerateValidCountry("China", 10000)
                },
                new List<string>
                {
                    "Percentual da Diferença em casos ativos entre: Brazil e Spain = 94,88%",
                    "Percentual da Diferença em casos ativos entre: Spain e USA = 439,37%",
                    "Percentual da Diferença em casos ativos entre: USA e Japan = 4.067,05%",
                    "Percentual da Diferença em casos ativos entre: Japan e China = 56,31%"
                }),
                
                (new List<Country>
                {
                    GenerateValidCountry("World", 5164312),
                    GenerateValidCountry("USA", 64623),
                    GenerateValidCountry("India", 57451),
                    GenerateValidCountry("Brazil", 5161),
                    GenerateValidCountry("France", 846)
                },
                new List<string>
                {
                    "Percentual da Diferença em casos ativos entre: World e USA = 7.891,45%",
                    "Percentual da Diferença em casos ativos entre: USA e India = 12,48%",
                    "Percentual da Diferença em casos ativos entre: India e Brazil = 1.013,18%",
                    "Percentual da Diferença em casos ativos entre: Brazil e France = 510,05%"
                })
            };
        }
        
        [Theory]
        [MemberData(nameof(PercentageDiference))]
        public void PercentageDiference_is_valid((List<Country> countries, List<string> expected) data)
        {
            // Dado / Setup
            repository.Setup(x => x.GetAll()).Returns(data.countries);
            
            // Quando / Ação
            var response = countriesService.PercentageDiference();

            // Deve / Asserções
            Assert.Equal(data.expected, response);
        }
    }
}
