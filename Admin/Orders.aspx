<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Store.Admin.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <main>
        <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">كل الطلبات
        </h1>

        <div class="addProduct w-full py-10 sm:px-10" style="direction: rtl">
            <div class="flex justify-between items-center">
                <h1 class="text-xl text-[#303841] font-black my-5">طلبات</h1>
                <h2>عدد العملاء <span id="cntClient" runat="server">10</span></h2>
            </div>
            <div class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">

                <!-- images -->
                <div class="p-8 rounded-md w-full">
                    <div class="mt-5">
                        <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                            <div class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                                <asp:SqlDataSource ID="sqlClient" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' ProviderName="System.Data.SqlClient"
                                    SelectCommand="SELECT Client.Id, Client.Name, COUNT(PId) AS CntOrder, SUM(Total) AS SumOrder, format(DateOr, 'dd/MM/yyyy') as DateOr, CASE WHEN Received = 1 THEN format(DateRe, 'dd/MM/yyyy') ELSE 'لم يستلم' END AS Received FROM Client INNER JOIN Orders ON Client.Id = Orders.CId GROUP BY Client.Id,Client.Name, Received, DateRe, DateOr ORDER BY Received"></asp:SqlDataSource>
                                <asp:GridView
                                    ID="gvClient"
                                    runat="server"
                                    DataSourceID="sqlClient"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="Id"
                                    CssClass="min-w-full leading-normal"
                                    OnRowCommand="gvClient_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>

                                        <asp:BoundField DataField="Name" HeaderText="الاسم" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CntOrder" HeaderText="عدد الطلبات" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="SumOrder" HeaderText="مجموع الطلبات" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="DateOr" HeaderText="تاريخ الطلب" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Received" HeaderText="استلام" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="التفاصيل" ItemStyle-CssClass="py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server"
                                                    CommandName="InfoClint"
                                                    CommandArgument='<%# Eval("Id") %>'>
                                                    <div class="text-center text-[18px]">
                                                    <i class="fa-solid fa-circle-info text-blue-900"></i>
                                                </div>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
