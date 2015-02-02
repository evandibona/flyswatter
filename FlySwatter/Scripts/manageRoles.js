$('div#manageroles').ready(rolesClickable)

function rolesClickable() {
    var users = {}
    var roles = ["Submitter", "Developer", "ProjectManager", "Admin"]
    $('div#manageroles tbody tr').each(function(i, e) {
        var user = jqToString(e)
        users[user] = {}
        $(e).children('td').each(function(x, y) {
            var attr = $(y).attr('id')
            alert(attr)
        })
    }) 
}

function jqToString(e) {
    $(e).text().trim() 
}