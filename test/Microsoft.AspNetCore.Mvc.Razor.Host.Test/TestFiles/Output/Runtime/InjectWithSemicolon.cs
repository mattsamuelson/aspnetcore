#pragma checksum "TestFiles/Input/InjectWithSemicolon.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc807ec0dc76610bdca62f482fefd7f584348df9"
namespace Asp
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Threading.Tasks;

    public class ASPV_TestFiles_Input_InjectWithSemicolon_cshtml : Microsoft.AspNetCore.Mvc.Razor.RazorPage<MyModel>
    {
        #line hidden
        public ASPV_TestFiles_Input_InjectWithSemicolon_cshtml()
        {
        }
        #line hidden
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public MyApp MyPropertyName { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public MyService<MyModel> Html { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public MyApp MyPropertyName2 { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public MyService<MyModel> Html2 { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }

        #line hidden

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
        }
        #pragma warning restore 1998
    }
}
