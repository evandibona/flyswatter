$('div#manageroles').ready(rolesClickable)

function rolesClickable() {
    var users = {}
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
}

function jqToString(e) {
    $(e).text().trim()
}

function flipRole(el) {
    var attr = $(el).attr('class')
    if (attr == 'nsel') {
        $(el).attr('class', 'sel')
        return 'rem'
    }
    if (attr == 'sel') {
        $(el).attr('class', 'nsel')
        return 'add'
    }
}

// Closures are not created with named functions. 