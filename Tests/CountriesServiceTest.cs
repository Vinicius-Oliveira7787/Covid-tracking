using System;
using System.Collections.Generic;
using Domain.Countries;
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
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(It.IsAny<Country>());

            // Quando / Ação
            var response = countriesService.Create(It.IsAny<string>());

            // Deve / Asserções
            Assert.Equal(new List<string>{"Country doesn't exists."}, response.Errors);
            repository.Verify(x => x.Add(It.IsAny<Country>()), Times.Never());
        }

        [Fact]
        public void Create_method_no_repeated_contries_error()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName("Brazil"))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Add(It.IsAny<Country>())).Throws(It.IsAny<Exception>());
            
            // Quando / Ação
            var response = countriesService.Create("Brazil");

            // Deve / Asserções
            Assert.Equal(new List<string>{"No repeated contries."}, response.Errors);
            repository.Verify(x => x.Add(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void Create_method_is_valid()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName("Brazil"))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Add(It.IsAny<Country>()));
            
            // Quando / Ação
            var response = countriesService.Create("Brazil");

            // Deve / Asserções
            Assert.Null(response.Errors);
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
            repository.Setup(x => x.Delete(It.IsAny<Guid>())).Throws<Exception>();
            
            // Quando / Ação
            var response = countriesService.Delete("Brazil");

            // Deve / Asserções
            Assert.False(response);
            repository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Delete_method_is_valid()
        {
            // Dado / Setup
            repository.Setup(x => x.Delete(It.IsAny<Guid>()));
            
            // Quando / Ação
            var response = countriesService.Delete("Brazil");

            // Deve / Asserções
            Assert.True(response);
            repository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Update_method_country_not_founded_error()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(It.IsAny<Country>());
            
            // Quando / Ação
            var response = countriesService.Update("Brazil");

            // Deve / Asserções
            Assert.False(response);
        }

        [Fact]
        public void Update_method_throws_exception()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(It.IsAny<Country>());
            repository.Setup(x => x.Update(It.IsAny<Country>())).Throws<Exception>();
            
            // Quando / Ação
            var response = countriesService.Update("Brazil");

            // Deve / Asserções
            Assert.False(response);
        }

        [Fact]
        public void Update_method_is_valid()
        {
            // Dado / Setup
            consumer.Setup((x => x.GetByName(It.IsAny<string>()))).Returns(GenerateValidCountry("Brazil"));
            repository.Setup(x => x.Update(It.IsAny<Country>()));
            
            // Quando / Ação
            var response = countriesService.Update("Brazil");

            // Deve / Asserções
            Assert.True(response);
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
