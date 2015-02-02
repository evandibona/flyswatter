var users = {}
$('div#manageroles').ready(rolesClickable)

function rolesClickable() {
    var roles = ["Submitter", "Developer", "ProjectManager", "Admin"]
    $('div#manageroles tbody tr').each(function (x, e) {
        var user = jqToString(e)
        users[user] = {}
        $(e).children('td').each(function (ri, q) {
            var role = roles[ri] 
            $(q).click(function() {
                var action = flipRole(q)
                users[user][role] = action
            }) 
        })
    })
    $('div#manageroles button').click(function () {
        $.post({
            url: "/EditRoles/Update", 
            data: users,

        })
    })
}

function jqToString(e) {
    return $(e).text().trim()
}

function flipRole(el) {
    var attr = $(el).attr('class')
    if (attr == 'nsel') {
        $(el).attr('class', 'sel')
        return 'add'
    }
    if (attr == 'sel') {
        $(el).attr('class', 'nsel')
        return 'rem'
    }
}

// Closures are not created with named functions. 