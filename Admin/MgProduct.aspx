<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="MgProduct.aspx.cs" Inherits="Store.Admin.MgProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">اضافة منتج
    </h1>

    <div class="addProduct w-full py-10 sm:px-10" style="direction: rtl">
        <h1 class="text-xl text-[#303841] font-black my-5">اضافة المنتجات</h1>
        <div
            class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">
            <!-- <h2>اضافة الصنف</h2> -->
            <div class="flex flex-wrap justify-between gap-10 mt-5">
                <div
                    class="flex flex-col justify-start items-center gap-10 w-full">
                    <!-- name product -->
                    <div
                        class="flex md:flex-row flex-col items-center gap-x-5 w-full">
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">اسم المنتج بالعربي</label>
                            <input id="txtNameAr" type="text" class="border-2 rounded w-3/4 p-2" runat="server" />
                        </div>

                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">اسم المنتج بالانكليزي</label>
                            <input id="txtNameEn" runat="server"
                                type="text"
                                class="border-2 rounded w-3/4 p-2 text-left" />
                        </div>
                    </div>
                    <!-- price -->
                    <div
                        class="flex md:flex-row flex-col items-center gap-x-5 w-full">
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">سعر المنتج</label>
                            <input id="txtPrice" runat="server"
                                type="text"
                                class="border-2 rounded w-3/4 p-2 text-left" />
                        </div>

                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">الخصم</label>
                            <input id="txtDisc" runat="server"
                                type="text"
                                class="border-2 rounded w-3/4 p-2 text-left" />
                        </div>
                    </div>
                    <!-- category -->
                    <div
                        class="flex md:flex-row flex-col items-center gap-x-5 w-full">
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="ddlDept" class="w-1/4">اختيار الصنف</label>
                            <asp:DropDownList ID="ddlDept" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlDept" DataTextField="NameAr" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlDept" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT [Id], [NameAr] FROM [Dept]" />
                        </div>
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="ddlBrand" class="w-1/4">القياس</label>
                            <asp:DropDownList ID="ddlSize" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlSizes" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                            <asp:SqlDataSource ID="sqlSizes" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT * FROM [Size]" />
                        </div>
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="ddlColor" class="w-1/4">اللون</label>
                            <asp:DropDownList ID="ddlColor" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlColors" DataTextField="Name" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlColors" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT * FROM [Color]" />
                        </div>
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="ddlBrand" class="w-1/4">اختيار الماركة</label>
                            <asp:DropDownList ID="ddlBrand" CssClass="bg-gray-100 border-2 w-3/4 p-2.5" runat="server" DataSourceID="sqlBrand" DataTextField="Name" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlBrand" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT * FROM [Brand]" />
                        </div>
                    </div>
                    <!-- info -->
                    <div
                        class="flex md:flex-row flex-col items-center gap-x-5 w-full">
                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">الوصف بالعربي</label>
                            <textarea id="txtInfoAr" runat="server" class="w-3/4" rows="5"></textarea>
                        </div>

                        <div class="flex items-center justify-center gap-2 w-full">
                            <label for="" class="w-1/4">الوصف بالانكليزي</label>
                            <textarea id="txtInfoEn" runat="server" class="w-3/4" rows="5"></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <!-- images -->
            <div class="p-8 rounded-md w-full">
                <div class="flex items-center justify-center gap-2 w-full">
                    <label for="<%= FileUpload2.ClientID %>" class="w-1/4">خلفية المنتج</label>
                    <asp:FileUpload ID="FileUpload2" CssClass="border-2 rounded w-3/4 p-2 text-left" runat="server" accept=".png,.jpg,.jpeg,.gif" />
                    <div id="preview2" class="flex justify-center p-3" runat="server"></div>
                </div>
                <div class="relative mt-10">

                    <div
                        class="chooseImage">
                        <asp:FileUpload ID="FileUpload1" CssClass="hidden" runat="server" AllowMultiple="true" accept=".png,.jpg,.jpeg,.gif" />
                        <label
                            id="file-input-label"
                            for="<%= FileUpload1.ClientID %>"
                            class="rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">
                            اضافة صورة</label>
                    </div>
                </div>
                <div class="mt-5">
                    <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                        <div
                            class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                            <div id="preview" class="flex justify-center p-3"></div>
                            <hr />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <asp:SqlDataSource ID="sqlImgProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                                        SelectCommand="SELECT * FROM [ProductPhotos] WHERE ([PId] = @PId) ORDER BY [Id]"
                                        DeleteCommand="DELETE FROM [ProductPhotos] WHERE [Id] = @Id"
                                        InsertCommand="INSERT INTO [ProductPhotos] ([Name], [ContentType], [Photo], [PId]) VALUES (@Name, @ContentType, @Photo, @PId);"
                                        OnInserting="sqlImgProduct_Inserting">
                                        <DeleteParameters>
                                            <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                            <asp:Parameter Name="ContentType" Type="String"></asp:Parameter>
                                            <asp:Parameter Name="Photo" Type="Object"></asp:Parameter>
                                            <asp:Parameter Name="PId" Type="Int32"></asp:Parameter>
                                        </InsertParameters>
                                        <SelectParameters>
                                            <asp:QueryStringParameter QueryStringField="id" DefaultValue="0" Name="PId" Type="Int32"></asp:QueryStringParameter>
                                        </SelectParameters>
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
                <asp:SqlDataSource ID="sqlBgProduct" runat="server" 
                    UpdateCommand="UPDATE Product SET Photo = @Photo WHERE (Id = @Id)" OnUpdating="sqlBgProduct_Updating"
                    ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' >
                    <UpdateParameters>
                        <asp:Parameter Name="Photo" Type="Object"></asp:Parameter>
                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqlProduct" runat="server"
                    ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                    DeleteCommand="DELETE FROM [Product] WHERE [Id] = @Id"
                    InsertCommand="INSERT INTO [Product] ([NameAr], [NameEn], [Price], [Discount], [infoAr], [infoEn], [Photo], [DId], [BId], [CId], [SId]) VALUES (@NameAr, @NameEn, @Price, @Discount, @infoAr, @infoEn, @Photo, @DId, @BId, @CId, @SId); SELECT @Identity=Scope_Identity();"
                    OnInserted="sqlProduct_Inserted" OnInserting="sqlProduct_Inserting" OnUpdated="sqlProduct_Updated"
                    SelectCommand="SELECT * FROM [Product] WHERE ([Id] = @Id)"
                    UpdateCommand="UPDATE [Product] SET [NameAr] = @NameAr, [NameEn] = @NameEn, [Price] = @Price, [Discount] = @Discount, [infoAr] = @infoAr, [infoEn] = @infoEn, [DId] = @DId, [BId] = @BId, [CId] = @CId, [SId] = @SId WHERE [Id] = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="NameAr" Type="String"></asp:Parameter>
                        <asp:Parameter Name="NameEn" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Price" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="Discount" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="infoAr" Type="String"></asp:Parameter>
                        <asp:Parameter Name="infoEn" Type="String"></asp:Parameter>
                        <asp:Parameter Name="DId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="BId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="CId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="SId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="Identity" Type="Int32" Direction="Output"></asp:Parameter>
                    </InsertParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter QueryStringField="id" Name="Id" Type="Int32"></asp:QueryStringParameter>
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="NameAr" Type="String"></asp:Parameter>
                        <asp:Parameter Name="NameEn" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Price" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="Discount" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="infoAr" Type="String"></asp:Parameter>
                        <asp:Parameter Name="infoEn" Type="String"></asp:Parameter>
                        <asp:Parameter Name="DId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="BId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="CId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="SId" Type="Int32"></asp:Parameter>
                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <button id="btnAdd" type="button" runat="server" onserverclick="btnAdd_ServerClick"
                            class="bg-[#0074D9] mt-3 text-white rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">
                            اضافة بيانات
                        </button>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAdd" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
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
        function previewImages2() {

            var preview = document.querySelector('#<%= preview2.ClientID%> ');
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
        document.querySelector('#ContentMain_FileUpload2').addEventListener("change", previewImages2);
    </script>
</asp:Content>
