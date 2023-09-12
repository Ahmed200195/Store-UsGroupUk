<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="LinkProduct.aspx.cs" Inherits="Store.Admin.LinkProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <main>
        <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">اضافة منتج
        </h1>

        <div class="w-full py-10 sm:px-10" style="direction: rtl">
            <h1 class="text-xl text-[#303841] font-black my-5">اضافة المنتجات</h1>
            <div
                class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">
                <div class="flex flex-wrap justify-between gap-10 mt-5">
                    <div
                        class="flex flex-col justify-start items-center gap-10 w-full">
                        <!-- name product -->
                        <div
                            class="flex md:flex-row flex-col items-center gap-x-5 w-full">
                            <div class="flex items-center justify-center w-full">
                                <label for="select" class="font-semibold block py-2 w-1/4">
                                    الأصناف</label>

                                <div class="relative w-full">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDept" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlDept" DataTextField="DeptName" DataValueField="Id" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    <asp:SqlDataSource ID="sqlDept" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT [Id], [NameAr] + ' | ' + [NameEn] AS DeptName FROM [Dept]" />
                                </div>
                            </div>

                            <div
                                class="flex items-center justify-center w-full lg:mt-0 mt-2">
                                <label for="select" class="font-semibold block py-2 w-1/4">
                                    المنتجات</label>

                                <div class="relative w-full">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>

                                            <asp:DropDownList ID="ddlProduct" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlProduct" DataTextField="ProdcutName" DataValueField="Id">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:SqlDataSource ID="sqlProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT [Id], [NameAr] + ' | ' + [NameEn] AS ProdcutName FROM [Product] WHERE DId = @DId">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlDept" DbType="Int32" DefaultValue="0" Name="DId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- send data -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <button id="btnAddToLink" class="mt-10 rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer bg-[#3a4750] text-[#f3f3f3]" runat="server" onserverclick="btnAddToLink_ServerClick">
                            اضافة
                        </button>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- images -->
                <div class="rounded-md w-full">
                    <div class="mt-5">
                        <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                            <div
                                class="inline-block min-w-full shadow rounded-lg overflow-hidden">

                                <asp:SqlDataSource ID="sqlLinkProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                                    SelectCommand="SELECT [Product].[Id], [NameAr], [Price], [Discount], ISNULL([Photo],'x') as [Photo] FROM [Product] WHERE [Id] IN (0) ORDER BY [Id] DESC"
                                     InsertCommand="INSERT INTO LinkProduct VALUES(@Id, @PId)">
                                    <InsertParameters>
                                        <asp:Parameter Name="Id" DbType="Int32" />
                                        <asp:Parameter Name="PId" DbType="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:GridView
                                            ID="gvProduct"
                                            runat="server"
                                            DataSourceID="sqlLinkProduct"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="Id"
                                            CssClass="min-w-full leading-normal" OnRowCommand="gvProduct_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>

                                                <asp:TemplateField HeaderText="خلفية" ItemStyle-CssClass="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                                    <ItemTemplate>
                                                        <div class="flex items-center">
                                                            <div class="flex-shrink-0 w-24 h-20">
                                                                <img class="w-full h-full rounded" src='data:image;base64,<%# Convert.ToBase64String((byte[])Eval("Photo")) %>' />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="NameAr" HeaderText="اسم المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Price" HeaderText="سعر المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="إزالة" ItemStyle-CssClass="py-5 border-b border-r border-gray-200 bg-white text-sm">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server"
                                                            CommandName="RemoveProduct"
                                                            CommandArgument='<%# Eval("Id") %>'>
                                                    <div class="text-center text-[18px]">
                                                    <i class="deleteBtn fa-solid fa-trash-can text-[#FF0000]" runat="server"></i>
                                                </div>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <button id="btnAddLinkData" runat="server" onserverclick="btnAddLinkData_ServerClick"
                        class="bg-[#0074D9] mt-3 text-white rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">
                        اضافة بيانات
                    </button>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
