using Bolt.ServerDrivenUI.Core;
using Shouldly;

namespace Bolt.ServerDrivenUI.Tests;

public class SectionInfoExtensions_IsApplicableTests
{
    
    private static RequestData defaultRequestData = new ()
    {
        Mode = RequestMode.Default,
        RootApp = "root-app",
        App = "app",
        Tenant = "tenant",
        CorrelationId = "correlation-id"
    };
    
    public class Request_mode_is_default
    {
        private RequestData givenRequestData = defaultRequestData with
        {
            Mode = RequestMode.Default
        };
        
        [Fact]
        public void Return_true_when_sections_are_empty()
        {
            SectionInfo[] givenEmptySection = [];
            var got = givenEmptySection.IsApplicable(givenRequestData);
            got.ShouldBeTrue();
        }
        
        [Fact]
        public void Return_true_when_sections_has_at_least_one_default_scope()
        {
            SectionInfo[] givenEmptySection = [ 
                new()
                {
                    Name = "section-1", Scope = SectionScope.Default
                }
            ];
            
            var got = givenEmptySection.IsApplicable(givenRequestData);
            
            got.ShouldBeTrue();
        }
        
        [Fact]
        public void Return_true_when_sections_has_at_least_one_always_scope()
        {
            SectionInfo[] givenEmptySection = [ 
                new()
                {
                    Name = "section-1", Scope = SectionScope.Always
                }
            ];
            
            var got = givenEmptySection.IsApplicable(givenRequestData);
            
            got.ShouldBeTrue();
        }

        [Fact]
        public void Return_false_when_sections_has_only_one_section_with_section_only_scope()
        {
            SectionInfo[] givenSections = [ new()
            {
                Name = "testing-1",
                Scope = SectionScope.SectionsOnly
            }];
            
            givenSections.IsApplicable(givenRequestData).ShouldBeFalse();
        }

        [Fact]
        public void Return_false_when_sections_has_only_one_section_with_lazy_scope()
        {
            SectionInfo[] givenSections = [ new()
            {
                Name = "testing-1",
                Scope = SectionScope.Lazy
            }];
            
            givenSections.IsApplicable(givenRequestData).ShouldBeFalse();
        }

        [Fact]
        public void Return_true_when_sections_has_only_one_section_with_always_scope()
        {
            SectionInfo[] givenSections = [ new()
            {
                Name = "testing-1",
                Scope = SectionScope.Always
            }];
            
            givenSections.IsApplicable(givenRequestData).ShouldBeTrue();
        }

        [Fact]
        public void Return_true_when_sections_has_only_one_section_with_default_scope()
        {
            SectionInfo[] givenSections = [ new()
            {
                Name = "testing-1",
                Scope = SectionScope.Default
            }];
            
            givenSections.IsApplicable(givenRequestData).ShouldBeTrue();
        }
        
        [Fact]
        public void Return_false_when_sections_has_no_default_or_always_scope()
        {
            SectionInfo[] givenEmptySection = [ 
                new()
                {
                    Name = "section-1", Scope = SectionScope.Lazy
                },
                new()
                {
                    Name = "section-1", Scope = SectionScope.SectionsOnly
                }
            ];
            
            var got = givenEmptySection.IsApplicable(givenRequestData);
            
            got.ShouldBeFalse();
        }
    }
    
    public class Request_mode_is_sections()
    {
        private static readonly string[] DefaultRequestSectionNames = ["section-1"];
        private RequestData givenRequestData = defaultRequestData with
        {
            Mode = RequestMode.Sections,
            SectionNames = DefaultRequestSectionNames
        };

        [Fact]
        public void Return_false_when_sections_are_empty()
        {
            SectionInfo[] givenEmptySections = [];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeFalse();
        }

        [Fact]
        public void Return_true_when_sections_no_match_and_has_at_least_one_always_scope()
        {
            SectionInfo[] givenEmptySections = [new()
            {
                Name = "non-matching",
                Scope = SectionScope.Always
            }];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeTrue();
        }
        
        [Fact]
        public void Return_false_when_sections_has_match_and_scope_is_lazy()
        {
            SectionInfo[] givenEmptySections = [new()
            {
                Name = DefaultRequestSectionNames[0],
                Scope = SectionScope.Lazy
            }];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeFalse();
        }
        
        [Fact]
        public void Return_true_when_sections_has_match_and_scope_is_default()
        {
            SectionInfo[] givenEmptySections = [new()
            {
                Name = DefaultRequestSectionNames[0],
                Scope = SectionScope.Default
            }];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeTrue();
        }

        [Fact]
        public void Return_true_when_section_with_default_scope_match()
        {
            SectionInfo[] givenEmptySections = [
                new()
                {
                    Name = "section-1",
                    Scope = SectionScope.Default
                },
                new()
                {
                    Name = "section-2",
                    Scope = SectionScope.Default
                }
            ];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeTrue();
        }
        
        [Fact]
        public void Return_false_when_no_sections_match_and_scope_of_section_is_default()
        {
            SectionInfo[] givenEmptySections = [
                new()
                {
                    Name = "section-no-match",
                    Scope = SectionScope.Default
                }
            ];
            givenEmptySections.IsApplicable(givenRequestData).ShouldBeFalse();
        }
    }
    
    public class Request_mode_is_lazy_sections
    {
        private const string DefaultSectionRequested = "section-1";
        private readonly RequestData _givenRequestData = defaultRequestData with
        {
            Mode = RequestMode.LazySections,
            SectionNames = [DefaultSectionRequested]
        };
        
        [Fact]
        public void Return_false_when_sections_are_empty()
        {
            SectionInfo[] givenSections = [];
            givenSections.IsApplicable(_givenRequestData).ShouldBeFalse();
        }
        
        [Fact]
        public void Return_false_when_no_match_and_section_scope_is_default()
        {
            SectionInfo[] givenSections = [
                new()
                {
                    Name = "no-match",
                    Scope = SectionScope.Default
                }
            ];
            givenSections.IsApplicable(_givenRequestData).ShouldBeFalse();
        }
        
        [Fact]
        public void Return_true_when_no_match_and_section_scope_is_always()
        {
            SectionInfo[] givenSections = [
                new()
                {
                    Name = "no-match",
                    Scope = SectionScope.Always
                }
            ];
            givenSections.IsApplicable(_givenRequestData).ShouldBeTrue();
        }
        
        [Fact]
        public void Return_false_when_no_match_and_section_scope_is_sectionsOnly()
        {
            SectionInfo[] givenSections = [
                new()
                {
                    Name = "no-match",
                    Scope = SectionScope.SectionsOnly
                }
            ];
            givenSections.IsApplicable(_givenRequestData).ShouldBeFalse();
        }
        
        [Fact]
        public void Return_false_when_no_match_and_section_scope_is_lazy()
        {
            SectionInfo[] givenSections = [
                new()
                {
                    Name = "no-match",
                    Scope = SectionScope.Lazy
                }
            ];
            givenSections.IsApplicable(_givenRequestData).ShouldBeFalse();
        }
        
        [Fact]
        public void Return_true_when_match_and_section_scope_is_default()
        {
            SectionInfo[] givenSections = [
                new()
                {
                    Name = DefaultSectionRequested,
                    Scope = SectionScope.Default
                }
            ];
            givenSections.IsApplicable(_givenRequestData).ShouldBeTrue();
        }
    }
}