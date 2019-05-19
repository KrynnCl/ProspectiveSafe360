using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProspectiveSafe360
{
    public class PersonNameFormatter
    {
        public string Format(string firstName, string lastName)
        {
            //Check if is the parameter are null or whiteSpance
            if ((firstName == null) || (lastName == null) || (firstName == " ") ||(lastName == " "))
            {
                throw new ArgumentException("The firstName and/or lastName must not be null or whitespace");

            }

            //Check if is the parameter are empty
            if ((firstName == string.Empty) || (lastName == string.Empty))
            {
                throw new ArgumentException("The firstName and/or lastName must not be empty");
            }

            //Take out the Spaces
            firstName = firstName.Trim().ToLower();
            lastName = lastName.Trim().ToLower();

            //Ensure the firstLetter is UpperCase
            //Ensured thefirst of each group
            
            firstName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(firstName);
            lastName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(lastName);

            //Check if is the parameter have invalid Characters 
            if ((Regex.IsMatch(firstName, @"[^\w\s\.-]")) || (Regex.IsMatch(lastName, @"[^\w\s\.-]")))
            {
                throw new ArgumentException("The firstName and/or lastName must not have invalid characters");
            }
            //Check if how manny special Characters (-._ ) have
            if ((Regex.Matches(firstName, @"[\s\.-]").Count >1) || (Regex.Matches(lastName, @"[\s\.-]").Count>1))
            {
                throw new ArgumentException("The firstName or lastName must not have more than one special character");
            }
            
            //Check if is the parameter have numbers
            if ((Regex.IsMatch(firstName, @"\d")) || (Regex.IsMatch(lastName, @"\d")))
            {
                throw new ArgumentException("The firstName and/or lastName must not have numbers");
            }

            

            return lastName + ", " + firstName;
        }
    }
}
