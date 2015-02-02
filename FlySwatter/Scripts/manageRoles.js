$('div#manageroles').ready(rolesClickable)

function rolesClickable() {
    var users = {}
    var roles = ["Submitter", "Developer", "ProjectManager", "Admin"]
    $('div#manageroles tbody tr').each(function (i, e) {
        var user = jqToString(e)
        users[user] = {}
        $(e).children('td').each(function (x, y) {
            $(y).click(function() {
                flipRole(y)
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
    }
    if (attr == 'sel') {
        $(el).attr('class', 'nsel')
    }
}

// Closures are not created with named functions. 