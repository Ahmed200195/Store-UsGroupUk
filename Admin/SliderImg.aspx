<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="SliderImg.aspx.cs" Inherits="Store.Admin.SliderImg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">الشريط
    </h1>
    <div class="w-full py-10 sm:px-10">
        <div class="container mx-auto">
            <!-- start in table slider -->
            <div class="p-8 rounded-md w-full">
                <div class="relative mt-10">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="chooseImage">
                                <asp:FileUpload ID="FileUpload1" CssClass="hidden" runat="server" AllowMultiple="true" accept=".png,.jpg,.jpeg,.gif" />
                                <label
                                    for="<%= FileUpload1.ClientID %>"
                                    class="rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">
                                    اضافة صورة</label>
                                <button id="btnAdd" type="button" runat="server" onserverclick="btnAdd_ServerClick"
                                    class="bg-[#0074D9] mt-3 text-white rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">
                                    رفع
                                </button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="mt-5">
                    <div id="preview" class="flex justify-center p-3"></div>
                    <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                        <div
                            class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <asp:SqlDataSource ID="sqlImgProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                                        SelectCommand="SELECT Id, Name, ContentType, Photo FROM ProductPhotos WHERE (PId IS NULL) ORDER BY Id"
                                        DeleteCommand="DELETE FROM [ProductPhotos] WHERE [Id] = @Id"
                                        InsertCommand="INSERT INTO [ProductPhotos] ([Name], [ContentType], [Photo]) VALUES (@Name, @ContentType, @Photo);"
                                        OnInserting="sqlImgProduct_Inserting">
                                        <DeleteParameters>
                                            <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                            <asp:Parameter Name="ContentType" Type="String"></asp:Parameter>
                                            <asp:Parameter Name="Photo" Type="Object"></asp:Parameter>
                                        </InsertParameters>
                                    </asp:SqlDataSource>
                                    <asp:GridView ID="gvImages" CssClass="min-w-full leading-normal" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="sqlImgProduct" OnRowCommand="gvImages_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" Visible="false"></asp:BoundField>
                                            <asp:TemplateField HeaderText="الصورة" ItemStyle-CssClass="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                                <ItemTemplate>
                                                    <div class="flex items-center">
                                                        <div class="flex-shrink-0 w-24 h-20">
                                                            <img class="w-full h-full rounded" src='data:image;base64,<%# Convert.ToBase64String((byte[])Eval("Photo")) %>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Name" HeaderText="الاسم" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                                <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="ContentType" HeaderText="النوع" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                                <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="حذف" ItemStyle-CssClass="py-5 border-b border-r border-gray-200 bg-white text-sm">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server"
                                                        CommandName="DelPhoto"
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
            </div>
            <!-- end in table slider -->
        </div>
        <!-- start no data -->
        <div id="dvNoData" class="noData flex flex-col justify-center items-center" runat="server">
            <img src="../images/admin/noData.svg" class="w-2/4 h-96" />
            <div class="flex flex-col justify-center items-center mt-5">
                <h1 class="capitalize text-2xl text-center font-black">لا توجد صور لعرضها
                </h1>
            </div>
        </div>
        <!-- end no data -->
    </div>
    <script>
        function previewImages() {

            var preview = document.querySelector('#preview');
            preview.innerHTML = "";
            if (this.files) {
                [].forEach.call(this.files, readAndPreview);
            }

            function readAndPreview(file) {

                // File type validator based on the extension 
                if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
                    return alert(file.name + " is not an image");
                }

                var reader = new FileReader();

                reader.addEventListener("load", function () {
                    var image = new Image();
                    image.height = 100;
                    image.width = 100;
                    image.title = file.name;
                    image.src = this.result;
                    preview.appendChild(image);
                });

                reader.readAsDataURL(file);

            }

        }

        document.querySelector('#ContentMain_FileUpload1').addEventListener("change", previewImages);
    </script>
</asp:Content>
