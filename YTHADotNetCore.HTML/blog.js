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
    confirmMessage("Are you sure want to delete?").then(
        function (value) {
            let lst = GetBlogs();

            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log("no data found.");
                return;
            }

            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);

            successMessage("Deleting Successful.");

            getBlogTable();
        }
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
