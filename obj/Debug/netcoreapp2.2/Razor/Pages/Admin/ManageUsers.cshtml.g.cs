#pragma checksum "G:\freelaunceProject\E-MedicalClaimRefundSystem\EMedicalClaimRefundSystem\Pages\Admin\ManageUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2b3b31d438f377053950065b717f82211dc3894"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EMedicalClaimRefundSystem.Pages.Admin.Pages_Admin_ManageUsers), @"mvc.1.0.razor-page", @"/Pages/Admin/ManageUsers.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Admin/ManageUsers.cshtml", typeof(EMedicalClaimRefundSystem.Pages.Admin.Pages_Admin_ManageUsers), null)]
namespace EMedicalClaimRefundSystem.Pages.Admin
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "G:\freelaunceProject\E-MedicalClaimRefundSystem\EMedicalClaimRefundSystem\Pages\_ViewImports.cshtml"
using EMedicalClaimRefundSystem;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2b3b31d438f377053950065b717f82211dc3894", @"/Pages/Admin/ManageUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e6c7be0fa33978710ec5ee38ed5852939d81676", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Admin_ManageUsers : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(184, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 6 "G:\freelaunceProject\E-MedicalClaimRefundSystem\EMedicalClaimRefundSystem\Pages\Admin\ManageUsers.cshtml"
  
    ViewData["Title"] = "Manage User Page";

#line default
#line hidden
            BeginContext(238, 8, true);
            WriteLiteral("\r\n\r\n<h1>");
            EndContext();
            BeginContext(247, 17, false);
#line 11 "G:\freelaunceProject\E-MedicalClaimRefundSystem\EMedicalClaimRefundSystem\Pages\Admin\ManageUsers.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(264, 40, true);
            WriteLiteral("</h1>\r\n\r\n\r\n<div class=\"container\">\r\n    ");
            EndContext();
            BeginContext(305, 23, false);
#line 15 "G:\freelaunceProject\E-MedicalClaimRefundSystem\EMedicalClaimRefundSystem\Pages\Admin\ManageUsers.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(328, 12507, true);
            WriteLiteral(@"
    <br />
    <div class=""row"">
        <a href=""/Admin/AddAccount"" class=""col-sm-2 btn btn-primary"">Add+</a>
        <div class=""col-sm-6""></div>
        <input class=""col-sm-2 btn btn-danger "" type=""button"" value=""Delete"" onclick=""DeleteUsers()""/>
        <input class=""col-sm-2 btn btn-info"" type=""button"" value=""Disable"" onclick=""BlockUsers()""/>
</div>
    <br />
    <div style=""width:90%; margin:0 auto;"">
        <table id=""usersTable"" class=""table table-striped table-bordered dt-responsive nowrap"" cellspacing=""0"">
            <thead class=""thead-dark"">
                <tr>
                    <th><input name=""select_all"" value=""1"" type=""checkbox""></th>
                    <th>ID</th>
                    <th>Full Name</th>
                    <th>Email</th>
                    <th>Role</th>
                    <th>Account Status</th>
                </tr>
            </thead>
        </table>
    </div>
</div>



    <script>

    var rows_selected = [];
    let table;
    ");
            WriteLiteral(@"//
    // Updates ""Select all"" control in a data table
    //
    function updateDataTableSelectAllCtrl(table){
       var $table             = table.table().node();
       var $chkbox_all        = $('tbody input[type=""checkbox""]', $table);
       var $chkbox_checked    = $('tbody input[type=""checkbox""]:checked', $table);
       var chkbox_select_all  = $('thead input[name=""select_all""]', $table).get(0);

       // If none of the checkboxes are checked
       if($chkbox_checked.length === 0){
          chkbox_select_all.checked = false;
          if('indeterminate' in chkbox_select_all){
             chkbox_select_all.indeterminate = false;
          }

       // If all of the checkboxes are checked
       } else if ($chkbox_checked.length === $chkbox_all.length){
          chkbox_select_all.checked = true;
          if('indeterminate' in chkbox_select_all){
             chkbox_select_all.indeterminate = false;
          }

       // If some of the checkboxes are checked
       } else ");
            WriteLiteral(@"{
          chkbox_select_all.checked = true;
          if('indeterminate' in chkbox_select_all){
             chkbox_select_all.indeterminate = true;
          }
       }
    }
    $(document).ready(function () {            
            //$.ajax({
            //    type: ""POST"",
            //    beforeSend: function (xhr) {
            //        xhr.setRequestHeader(""X-XSRF-Token"",
            //            $('input:hidden[name=""__RequestVerificationToken""]').val());
            //    },
            //    url: ""/Admin/ManageUsers?handler=GetUsers"",
            //    contentType: ""application/json; charset=utf-8"",
            //    dataType: ""json"",
            //    success: function (response) {
            //        var jsonObject = $.parseJSON(JSON.stringify(response));                    
            //        table = $('#usersTable').DataTable({
            //            data: jsonObject,
            //            //data : response,
            //            columns: [            ");
            WriteLiteral(@"                
            //                {
            //                    data:   ""Id"",
            //                    render: function ( data, type, row ) {
                                    
            //                            return '<input id='+data+' type=""checkbox"">';                                    
            //                    },                                
            //                },
            //                {
            //                    ""render"": function (data, type, full, meta) {
            //                        return meta.row;
            //                    }
            //                },
            //                { ""data"": ""FullName"" },
            //                { ""data"": ""Email"" },
            //                {
            //                    ""data"": ""Roles"",
            //                    ""render"": function (data, type, full, meta) {
            //                        return JSON.stringify(data);
 ");
            WriteLiteral(@"           //                    }
            //                }
            //            ],
            //            searching: true                       
            //        });

            //        // Handle click on checkbox
            //       $('#usersTable tbody').on('click', 'input[type=""checkbox""]', function(e){
            //          var $row = $(this).closest('tr');

            //          // Get row data
            //          var data = table.row($row).data();

            //          // Get row ID
            //          var rowId = data.Id;

            //          // Determine whether row ID is in the list of selected row IDs 
            //          var index = $.inArray(rowId, rows_selected);

            //          // If checkbox is checked and row ID is not in list of selected row IDs
            //          if(this.checked && index === -1){
            //             rows_selected.push(rowId);

            //          // Otherwise, if checkbox is not c");
            WriteLiteral(@"hecked and row ID is in list of selected row IDs
            //          } else if (!this.checked && index !== -1){
            //             rows_selected.splice(index, 1);
            //          }

            //          if(this.checked){
            //             $row.addClass('selected');
            //          } else {
            //             $row.removeClass('selected');
            //          }

            //          // Update state of ""Select all"" control
            //          updateDataTableSelectAllCtrl(table);

            //          // Prevent click event from propagating to parent
            //          e.stopPropagation();
            //       });


            //        // Handle click on ""Select all"" control
            //       $('thead input[name=""select_all""]', table.table().container()).on('click', function(e){
            //          if(this.checked){
            //             $('#usersTable tbody input[type=""checkbox""]:not(:checked)').trigger('click'");
            WriteLiteral(@");
            //          } else {
            //             $('#usersTable tbody input[type=""checkbox""]:checked').trigger('click');
            //          }

            //          // Prevent click event from propagating to parent
            //          e.stopPropagation();
            //       });
            //    }
            //});


        //var jsonObject = $.parseJSON(JSON.stringify(response));                    
        table = $('#usersTable').DataTable({           
            ""processing"": true,
            ""serverSide"": false,
            ""ajax"": {
                ""url"": ""/Admin/ManageUsers?handler=Users"",
                ""type"": ""GET"",
                ""beforeSend"" : function (xhr) {
                    xhr.setRequestHeader(""X-XSRF-Token"",
                        $('input:hidden[name=""__RequestVerificationToken""]').val());
                },                
                ""dataSrc"": '',                               
            },
            //data : response,
");
            WriteLiteral(@"            searching: true,
            ""pageLength"": 50,
            columns: [                            
                {
                    data:   ""Id"",
                    render: function ( data, type, row ) {
                                    
                            return '<input id='+data+' type=""checkbox"">';                                    
                    }                    
                },
                {
                    ""render"": function (data, type, full, meta) {
                        return meta.row;
                    }
                },
                { ""data"": ""FullName"" },
                { ""data"": ""Email"" },
                {
                    ""data"": ""Roles"",
                    ""render"": function (data, type, full, meta) {
                        return JSON.stringify(data);
                    }
                },
                {
                    data: {status : ""Blocked"", id : ""Id""},
                    render: functio");
            WriteLiteral(@"n (data, type, row) {
                        if (data.Blocked)
                            return '<input id=' + data.Id + ' type=""Button"" value=""Unblock now!"" onclick=UnBlockUsers(this) class=""btn btn-secondary"">';
                        else
                            return 'Active';
                    }                    
                },
            ],                                   
        });

        // Handle click on checkbox
        $('#usersTable tbody').on('click', 'input[type=""checkbox""]', function(e){
            var $row = $(this).closest('tr');

            // Get row data
            var data = table.row($row).data();

            // Get row ID
            var rowId = data.Id;

            // Determine whether row ID is in the list of selected row IDs 
            var index = $.inArray(rowId, rows_selected);

            // If checkbox is checked and row ID is not in list of selected row IDs
            if(this.checked && index === -1){
                row");
            WriteLiteral(@"s_selected.push(rowId);

            // Otherwise, if checkbox is not checked and row ID is in list of selected row IDs
            } else if (!this.checked && index !== -1){
                rows_selected.splice(index, 1);
            }

            if(this.checked){
                $row.addClass('selected');
            } else {
                $row.removeClass('selected');
            }

            // Update state of ""Select all"" control
            updateDataTableSelectAllCtrl(table);

            // Prevent click event from propagating to parent
            e.stopPropagation();
        });


        // Handle click on ""Select all"" control
        $('thead input[name=""select_all""]', table.table().container()).on('click', function(e){
            if(this.checked){
                $('#usersTable tbody input[type=""checkbox""]:not(:checked)').trigger('click');
            } else {
                $('#usersTable tbody input[type=""checkbox""]:checked').trigger('click');
            }

            WriteLiteral(@"
            // Prevent click event from propagating to parent
            e.stopPropagation();
        });

        });
    function DeleteUsers() {
        
        var jsonselectedrows = JSON.stringify(rows_selected);
        alert(""CheckBox ids are "" + jsonselectedrows);
        $.ajax({
            type: ""POST"",
            beforeSend: function (xhr) {
                xhr.setRequestHeader(""X-XSRF-Token"",
                    $('input:hidden[name=""__RequestVerificationToken""]').val());
            },
            url: ""/Admin/ManageUsers?handler=DeleteUsers"",
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            data: jsonselectedrows,
            success: function (response) {
                table.ajax.reload();
            }
        });
            }
    function BlockUsers() {
        
        var jsonselectedrows = JSON.stringify(rows_selected);
        //alert(""CheckBox ids are "" + jsonselectedrows);
        $.ajax({
        ");
            WriteLiteral(@"    type: ""POST"",
            beforeSend: function (xhr) {
                xhr.setRequestHeader(""X-XSRF-Token"",
                    $('input:hidden[name=""__RequestVerificationToken""]').val());
            },
            url: ""/Admin/ManageUsers?handler=BlockUsers"",
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            data: jsonselectedrows,
            success: function (response) {
                table.ajax.reload();
            }
        });
            }
    function UnBlockUsers(button) {

        var jsonselectedrows = JSON.stringify(button.id);
        //alert(""CheckBox ids are "" + jsonselectedrows);
        $.ajax({
            type: ""POST"",
            beforeSend: function (xhr) {
                xhr.setRequestHeader(""X-XSRF-Token"",
                    $('input:hidden[name=""__RequestVerificationToken""]').val());
            },
            url: ""/Admin/ManageUsers?handler=UnBlockUsers"",
            contentType: ""application/json");
            WriteLiteral("; charset=utf-8\",\r\n            dataType: \"json\",\r\n            data: jsonselectedrows,\r\n            success: function (response) {\r\n                table.ajax.reload();\r\n            }\r\n        });\r\n    }\r\n    </script>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EMedicalClaimRefundSystem.Pages.ManageUsersModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EMedicalClaimRefundSystem.Pages.ManageUsersModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EMedicalClaimRefundSystem.Pages.ManageUsersModel>)PageContext?.ViewData;
        public EMedicalClaimRefundSystem.Pages.ManageUsersModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591