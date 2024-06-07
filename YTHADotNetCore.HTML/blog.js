const tblBlog = "blogs";
let blogId = null;
getBlogTable();
// CreateBlog();
// UpdateBlog("bf3b70dc-da0c-4fff-b7d3-b11baf272064","Hello","Ye","Love")
// DeleteBlog("223fd028-a85d-4af3-bb1b-dcc6dc5cd51b")

function ReadBlog(){
    GetBlogs();
}

function CreateBlog(title,author,content){
    
    let lst = GetBlogs();
    const requestData = {
        'id' : uuidv4(),
        'title' : title,
        'author' : author,
        'content' : content
    };

    lst.push(requestData);
    const jsonData = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonData)
    successMessage("Save Success.")
    clearControl();
}

function EditBlog(id){
    let lst = GetBlogs();
    const items = lst.filter(x => x.id === id);
    if(items.length == 0){
        console.log("No Data Found...");
        errorMessage("No Data Found...");
        return;
    }

    item = items[0];
    blogId = item.id
    $("#txtTitle").val(item.title);
    $("#txtAuthor").val(item.author);
    $("#txtContent").val(item.content);
}

function UpdateBlog(id,title,author,content){
    let lst = GetBlogs();
    
    const items = lst.filter(x => x.id === id);
    if(items.length == 0){
        console.log("No Data Found...");
        errorMessage("No Data Found...");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonData = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonData)
    successMessage("Update Success.");
}

function DeleteBlog(id){
    Swal.fire({
        title: "Are you sure?",
        text: "Do you want to delete it!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
      }).then((result) => {
        if (result.isConfirmed) {
            lst = GetBlogs(); 
            const items = lst.filter(x => x.id === id);
            if(items.length == 0){
                console.log("No Data Found...");
                return;
            }
            lst = lst.filter(x => x.id !== id);
            const jsonData = JSON.stringify(lst);
            localStorage.setItem(tblBlog,jsonData)
            successMessage("Delete Success.")
            getBlogTable();
        }
      });
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
  }

  function GetBlogs(){
    let blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }
    return lst;
}

function successMessage(message){
    Swal.fire({
        position: "center",
        icon: "success",
        title: message,
        showConfirmButton: false,
        timer: 1500
      });
    }

function errorMessage(message){
    Swal.fire({
        position: "center",
        icon: "error",
        title: message,
        showConfirmButton: false,
        timer: 1500
      });
}

function clearControl(){
    $("#txtTitle").val('');
    $("#txtAuthor").val('');
    $("#txtContent").val('');
}

function getBlogTable(){
    const lst = GetBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
            <tr>
                <td><input type="button" value="Edit" onclick="EditBlog('${item.id}')" class="btn btn-warning"></td>
                <td><input type="button" value="Delete" onclick="DeleteBlog('${item.id}')" class="btn btn-danger"></td>
                <td scope="row">${++count}</td>
                <td>${item.title}</td>
                <td>${item.author}</td>
                <td>${item.content}</td>
            </tr>
        `;
        htmlRows += htmlRow;
    });
    $("#tbody").html(htmlRows);
}

$("#btnSave").click(function(){
    const title = $("#txtTitle").val();
    const author = $("#txtAuthor").val();
    const content = $("#txtContent").val();
    if(blogId === null){
        CreateBlog(title,author,content)
    }
    else{
        UpdateBlog(blogId,title,author,content)
        blogId = null;
    }
    getBlogTable();
})
