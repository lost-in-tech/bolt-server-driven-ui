using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Tests;

public partial class UrlHelperTests
{
    public class AppendQueryStrings
    {
        [Fact]
        public void Should_return_correct_result_when_path_has_no_querystring_and_we_pass_null_query_strings()
        {
            Execute(givenPath: "/", 
                givenQueryStrings: null, 
                expected: "/");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_no_querystring_and_we_pass_empty_query_strings()
        {
            Execute(givenPath: "/", 
                givenQueryStrings: Enumerable.Empty<(string Key, string? Value)>(), 
                expected: "/");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_no_querystring_and_we_pass_query_strings_with_only_one_item_with_null_value()
        {
            Execute(givenPath: "/", 
                givenQueryStrings: new (string Key, string? Value)[]{("test", null)}, 
                expected: "/");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_no_querystring_and_we_pass_only_one_query_string()
        {
            Execute(givenPath: "/", 
                givenQueryStrings: new (string Key, string? Value)[]{("hello", "world")}, 
                expected: "/?hello=world");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_no_querystring_and_we_pass_two_query_strings()
        {
            Execute(givenPath: "/", 
                givenQueryStrings: new (string Key, string? Value)[]
                {
                    ("hello", "world"),
                    ("name", "ruhul")
                }, 
                expected: "/?hello=world&name=ruhul");
        }
        
        [Fact]
        public void Should_return_correct_result_with_encoded_query_value_that_passed_as_argument()
        {
            Execute(givenPath: "/?blah=not encoded", 
                givenQueryStrings: new (string Key, string? Value)[]
                {
                    ("msg", "hello world")
                }, 
                expected: "/?blah=not encoded&msg=hello%20world");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_existing_query_string_and_we_pass_no_query_strings()
        {
            Execute(givenPath: "/?test=1", 
                givenQueryStrings: null, 
                expected: "/?test=1");
        }
        
        [Fact]
        public void Should_return_correct_result_when_path_has_existing_query_string_and_we_pass_query_strings()
        {
            Execute(givenPath: "/?test=1", 
                givenQueryStrings: new (string Key, string? Value)[]
                {
                    ("hello","world"),
                    ("name", "ruhul")
                }, 
                expected: "/?test=1&hello=world&name=ruhul");
        }

        private void Execute(string givenPath, IEnumerable<(string Key, string? Value)>? givenQueryStrings, string expected)
        {
            var got = UrlHelper.AppendQueryString(givenPath, givenQueryStrings);
            
            got.ShouldBe(expected);
        }
    }
}