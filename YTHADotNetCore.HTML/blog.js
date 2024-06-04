const tblBlog = "blogs";
// CreateBlog();
// UpdateBlog("bf3b70dc-da0c-4fff-b7d3-b11baf272064","Hello","Ye","Love")
DeleteBlog("223fd028-a85d-4af3-bb1b-dcc6dc5cd51b")

function ReadBlog(){
    let blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
}

function CreateBlog(){
    let blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }

    const requestData = {
        'id' : uuidv4(),
        'title' : 'blog title',
        'author' : 'blog author',
        'content' : 'blog content'
    };

    lst.push(requestData);
    const jsonData = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonData)
}

function UpdateBlog(id,title,author,content){
    let blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }
    
    const items = lst.filter(x => x.id === id);
    if(items.length == 0){
        console.log("No Data Found...");
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
}

function DeleteBlog(id){
    let blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }
    
    const items = lst.filter(x => x.id === id);
    if(items.length == 0){
        console.log("No Data Found...");
        return;
    }
    lst = lst.filter(x => x.id !== id);
    const jsonData = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonData)
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
  }