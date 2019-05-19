using System;
using Xunit;
using ProspectiveSafe360;

namespace ProspectiveSafe360.Tests
{
    public class PersonNameFormatterTest
    {
        [Theory]
        [InlineData(" "," ")]
        [InlineData(" ", "Castillo")]
        [InlineData(" ", null)]
        [InlineData("Alex", " ")]
        [InlineData("Alex", null)]
        [InlineData(null, null)]
        [InlineData(null, " ")]
        [InlineData(null, "Castillo")]
        public void Throw_Argument_exception_when_is_Null_or_whiteSpace(string firstName ,string lastName)
        {
            var item = new PersonNameFormatter();

            Action act = () => item.Format(firstName, lastName);


            Exception ex = Assert.Throws<ArgumentException>(act);

            Assert.Equal("The firstName and/or lastName must not be null or whitespace", ex.Message);
        }

        [Theory]
        [InlineData("", "Castillo")]
        [InlineData("Alex", "")]
        [InlineData("", "")]
        public void Throw_Argument_exception_when_is_empty(string firstName, string lastName)
        {
            var item = new PersonNameFormatter();

            Action act = () => item.Format(firstName, lastName);


            Exception ex = Assert.Throws<ArgumentException>(act);

            Assert.Equal("The firstName and/or lastName must not be empty", ex.Message);
        }

        [Fact]
        public void Given_a_firstName_and_lastName_return_formatName()
        {
            var item = new PersonNameFormatter();
            var expected = "Castillo, Alex";

            
            Assert.Equal(expected, item.Format("Alex", "Castillo"));
        }

        [Theory]
        [InlineData("Pedro","Palote ","Palote, Pedro")]
        [InlineData("Pedro ", "Palote ", "Palote, Pedro")]
        [InlineData(" Pedro ", "Palote ", "Palote, Pedro")]
        [InlineData("Pedro", " Palote ", "Palote, Pedro")]
        public void Given_firstName_or_lastName_with_white_space_after_or_before_return_proper_formatName(string firstName , string lastName, string expected)
        {
            var item = new PersonNameFormatter();

            Assert.Equal(expected, item.Format(firstName, lastName));

        }

        [Theory]
        [InlineData("juan", "pizarro", "Pizarro, Juan")]
        [InlineData("juan", "Pizarro", "Pizarro, Juan")]
        [InlineData("Juan", "pizarro", "Pizarro, Juan")]
        public void Given_firstName_or_lastName_without_first_upperCase_return_proper_formatName(string firstName, string lastName, string expected)
        {
            var item = new PersonNameFormatter();

            Assert.Equal(expected, item.Format(firstName, lastName));
        }

        [Theory]
        [InlineData("A%lex","Casill")]
        [InlineData("Alex", "*Cas#ill")]
        [InlineData("A)lex", "Castillo")]
        [InlineData("A/lex", "Castillo")]
        [InlineData("A|lex", "Castillo")]
        [InlineData("Alex", "\\Castillo")]
        [InlineData("@lex", "Castillo")]
        [InlineData(" AlEx,castillo", "Castillo.HurtuBIA1")]
        public void Given_invalid_character_in_first_or_lastName_throw_argumentException(string firstName, string lastName)
        {
            var item = new PersonNameFormatter();

            Action act = () => item.Format(firstName, lastName);


            Exception ex = Assert.Throws<ArgumentException>(act);

            Assert.Equal("The firstName and/or lastName must not have invalid characters", ex.Message);

        }

        [Fact]        
        public void Given_number_in_first_or_lastName_throw_argumentException()
        {
            var item = new PersonNameFormatter();

            Action act = () => item.Format("Al3x", "C4st1ll0");


            Exception ex = Assert.Throws<ArgumentException>(act);

            Assert.Equal("The firstName and/or lastName must not have numbers", ex.Message);

        }

        [Theory]
        [InlineData("Alex David", "Castillo", "Castillo, Alex David")]
        [InlineData("Alex-David", "Castillo", "Castillo, Alex-David")]
        [InlineData("Alex.David", "Castillo", "Castillo, Alex.David")]
        public void Given_first_or_lastName_with_special_character_return_proper_format(string firstName, string lastName, string expected)
        {
            var item = new PersonNameFormatter();

            Assert.Equal(expected, item.Format(firstName, lastName));
        }

        [Theory]
        [InlineData("Alex", "CasTillo", "Castillo, Alex")]
        [InlineData("ALEX", "CasTilLO-Hurtubia", "Castillo-Hurtubia, Alex")]
        public void Given_first_or_lastName_with_upperCase_in_any_place(string firstName, string lastName, string expected)
        {
            var item = new PersonNameFormatter();

            Assert.Equal(expected, item.Format(firstName, lastName));
        }

        [Fact]
        public void Given_first_or_lastName_with_specialCharacters_without_first_capital_letter()
        {
            var item = new PersonNameFormatter();

            var expected = "Castillo Hurtubia, Alex";

            Assert.Equal(expected, item.Format("Alex", "Castillo hurtubia"));
        }

        
        [Fact]
        public void Given_more_than_one_special_character_should_throw_ArgumentException()
        {
            var item = new PersonNameFormatter();

            Action act = () => item.Format(" Alex", "Castillo.-.HurtuBIA");


            Exception ex = Assert.Throws<ArgumentException>(act);

            Assert.Equal("The firstName or lastName must not have more than one special character", ex.Message);
            
        }
    }
}
