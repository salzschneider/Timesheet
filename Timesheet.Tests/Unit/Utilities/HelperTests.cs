﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.UI.Models;
using Timesheet.UI.Utilities;
using Xunit;
using Newtonsoft.Json;

namespace Timesheet.Test.Utilities
{
    public class HelperTests 
    {
        public static IEnumerable<object[]> GetCreateUserModelInlineDataPassed =>
                new List<object[]>
                {
                    new object[] { new UsersFullDTO() { Id = 1, Username = "TestUsername", Password = "Password", FullName = "TestFullname"  },
                                   new UserModel() { Id = 1, Username = "TestUsername", Password = "Password", FullName = "TestFullname"  } },
                    new object[] { new UsersFullDTO() { Id = 1, Username = null, Password = "", FullName = "TestFullname1"  },
                                   new UserModel() { Id = 1, Username = null, Password = "", FullName = "TestFullname1"  } },
                };

        public static IEnumerable<object[]> GetCreateUserModelInlineDataFailed =>
                new List<object[]>
                {
                    new object[] { new UsersFullDTO() { Id = 1, Username = "TestUsernamew", Password = "Password", FullName = "TestFullname"  },
                                   new UserModel() { Id = 1, Username = "TestUsername", Password = "Password", FullName = "TestFullname"  } },
                    new object[] { new UsersFullDTO() { Id = 1, Username = "TestUsername1", Password = null, FullName = "TestFullname1"  },
                                   new UserModel() { Id = 1, Username = "TestUsername1", Password = "", FullName = "TestFullname1"  } },
                };

        [Theory]
        [InlineData("hello", "A", "AhelloA")]
        [InlineData("hello", "\"", "\"hello\"")]
        [InlineData("", "", "")]
        public void WrapString_AnyString_ReturnWrapptedString(string input, string wrapString, string expected)
        {
            //act
            var actual = Helper.WrapString(input, wrapString);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("hel\"lo", "hel\"\"lo")]
        [InlineData("hello", "hello")]
        public void EscapeQuote_AnyString_ReturnEscapedString(string input, string expected)
        {
            //act
            var actual = Helper.EscapeQuote(input);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetWrapListInlineDataPassed =>
            new List<object[]>()
            {
                new object[]{new List<string>(){"Hello", "", "O'Connor"}, "\"", new List<string>(){"\"Hello\"","\"\"", "\"O'Connor\"" } },
                new object[]{new List<string>(){"Hel\"lo", "", "O'Connor"}, "AA", new List<string>(){"AAHel\"loAA","AAAA", "AAO'ConnorAA" } },
            };

        [Theory]
        [MemberData(nameof(GetWrapListInlineDataPassed))]
        public void WrapListElements_ListWithStringElements_ReturnListWithWrappedElements(IEnumerable<string> list, string wrapString, IEnumerable<string> expected)
        {
            //act
            var actual = Helper.WrapListElements(list, wrapString);

            //assert
            Assert.True(Helper.ListsEqual(actual, expected));
        }

        public static IEnumerable<object[]> GetListsEqualInlineDataPassed =>
            new List<object[]>()
            {
                new object[]{ new List<string> { "first", "second", "o'connor" }, new List<string> { "second", "o'connor", "first" }, true },
                new object[]{ new List<string> { "", "second", "" }, new List<string> { "second", "", "" }, true },
            };

        [Theory]
        [MemberData(nameof(GetListsEqualInlineDataPassed))]
        public void ListsEqual_StringLists_ReturnIfListsEqual(IEnumerable<string> firstList, IEnumerable<string> secondList, bool expected)
        {
            //act
            var actual = Helper.ListsEqual(firstList, secondList);

            //assert
            Assert.Equal(actual, expected);
        }

        public static IEnumerable<object[]> GetArrayToCSVInlineDataPassed =>
            new List<object[]>()
            {
                new object[]{ new string[][] { new string[] {"Admin", "ddd", "00:10:00" }, new string[] { "Admin", "Meeting", "00:00:00" } },
                              "\"Admin\",\"ddd\",\"00:10:00\"" + "\r\n" + "\"Admin\",\"Meeting\",\"00:00:00\"" + "\r\n"},

                new object[]{ new string[][] { new string[] {"Admin", "", "00:10:00" }, new string[] { "Admin", "Meeting", "" } },
                              "\"Admin\",\"\",\"00:10:00\"" + "\r\n" + "\"Admin\",\"Meeting\",\"\"" + "\r\n"},
            };

        [Theory]
        [MemberData(nameof(GetArrayToCSVInlineDataPassed))]
        public void ArrayToCSV_ArrayWithoutHeader_ReturnCSVText(string[][] array, string expectd)
        {
            //act
            var actual = Helper.ArrayToCSV(array, null);

            //assert
            Assert.Equal(expectd, actual);
        }

        public static IEnumerable<object[]> GetArrayToCSVWithHeaderInlineDataPassed =>
            new List<object[]>()
            {
                new object[]{ new string[][] { new string[] {"Admin", "ddd", "00:10:00" }, new string[] { "Admin", "Meeting", "00:00:00" } },
                              new List<string>(){"Username", "Activity", "Duration"},
                              "\"Username\",\"Activity\",\"Duration\"" + "\r\n" + "\"Admin\",\"ddd\",\"00:10:00\"" + "\r\n" + "\"Admin\",\"Meeting\",\"00:00:00\"" + "\r\n"},

                new object[]{ new string[][] { new string[] {"Admin", "ddd", "00:10:00" }, new string[] { "Admin", "Meeting", "00:00:00" } },
                              new List<string>(){"Username", "", "Duration"},
                              "\"Username\",\"\",\"Duration\"" + "\r\n" + "\"Admin\",\"ddd\",\"00:10:00\"" + "\r\n" + "\"Admin\",\"Meeting\",\"00:00:00\"" + "\r\n"},
            };

        [Theory]
        [MemberData(nameof(GetArrayToCSVWithHeaderInlineDataPassed))]
        public void ArrayToCSV_ArrayWithHeader_ReturnCSVText(string[][] array, List<string> header, string expectd)
        {
            //act
            var actual = Helper.ArrayToCSV(array, header);

            //assert
            Assert.Equal(expectd, actual);
        }

        public class Person
        {
            public int Age { get; set; } = 10;
            public string Name = "Joe";
            public PersonSub PersonSub { get; set; } = new PersonSub();
            public PersonSub PersonNullSub { get; set; } = null;
        }

        public class PersonSub
        {
            public DateTime Date = new DateTime(2008, 3, 1, 7, 0, 0);
        }

        public static IEnumerable<object[]> GetCloneObjectInlineDataPassed =>
            new List<object[]>()
            {
                new object[]{ new Person()},
                new object[]{ new Object()},
            };

        [Theory]
        [MemberData(nameof(GetCloneObjectInlineDataPassed))]
        public void CloneObject_AnyObject_ReturnNewObjectWithSamePropertiesAndValues(object inputObject)
        {
            //act
            var actual = Helper.CloneObject(inputObject);

            var actualString = JsonConvert.SerializeObject(actual);
            var expectedString = JsonConvert.SerializeObject(inputObject);

            //assert
            Assert.Equal(actualString, expectedString);
        }

        [Theory]
        [InlineData(-10, "00:00:00")]
        [InlineData(0, "00:00:00")]
        [InlineData(60, "00:01:00")]
        [InlineData(3671, "01:01:11")]
        [InlineData(86401, "24:00:01")]
        [InlineData(396612, "110:10:12")]
        public void SecondsToHourMinSec_Seconds_ReturnHoursMinutesSecondsFormat(int seconds, string expected)
        {
            //act
            var actual = Helper.SecondsToHourMinSec(seconds);

            //assert
            Assert.Equal(expected, actual);
        }

    }
}
