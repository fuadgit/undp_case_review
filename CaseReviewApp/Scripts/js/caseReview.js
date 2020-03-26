// Member Submit
$('#btnMemberSubmit').on('click', function () {
    if (!$('#member-form').valid()) {
        return;
    }
    var data = $('#member-form').serialize();
    $.ajax({
        type: 'POST',
        url: '/Member/Insert',
        data: data,
        success: function (response) {
            $('#member-modal').modal('hide');
            $("#member-table").html(response)
        }
    });
    return false;
});
$('#member-modal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
})

//Case Submit
$('#btnCaseSubmit').on('click', function () {
    if (!$('#case-form').valid()) {
        return;
    }
    var data = $('#case-form').serialize();
    $.ajax({
        type: 'POST',
        url: '/Case/Insert',
        data: data,
        success: function (response) {
            $('#case-modal').modal('hide');
            $('#case-table').html(response);
        }
    });
    return false;
});
$('#case-modal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
})

//Review Panel
// Delete
var ShowDelModal = function (reviewId) {
    $('#hiddenReviewId').val(reviewId);
    $('#del-modal').modal('show');
}
var DeleteReview = function () {
    var reviewId = $('#hiddenReviewId').val();
    $.ajax({
        type: 'POST',
        url: '/ReviewPanel/Delete',
        data: { ReviewID: reviewId },
        success: function (response) {
            $('#del-modal').modal('hide');
            window.location.href = response.Url;
        }
    });
}

var ShowEditModal = function (reviewId) {
    var url = '/ReviewPanel/EditModalData?ReviewId=' + reviewId;
    $('#panel-edit-body').load(url, function () {
        $('#hiddenReviewId').val(reviewId);
        $('#panel-modal-edit').modal('show');
    });
}

$('#btnPanelSubmit').on('click', function () {
    if (!$('#panel-form').valid()) {
        return;
    }
    $('.case-list-msg').html('');
    var caseIds = [];
    var reviews = []
    $('.hiddenCaseId').each(function (index, val) {
        caseIds.push($(val).val());
    });
    caseIds.forEach(caseId => {
        var memberIds = [];
        var isChecked = $('#case_' + caseId).is(':checked');
        if (isChecked) {
            $(`#check_${caseId} .member input[type=checkbox]:checked`).each(function (index, val) {
                var splittedId = $(val).attr('id').split('_');
                var memberId = splittedId[splittedId.length - 1];
                memberIds.push(memberId)
            });
        }
        if (memberIds.length) {
            reviews.push({ CaseID: caseId, Members: memberIds });
        }
    });
    if (!reviews.length) {
        $('.case-list-msg').html('Please select Cases and their panel members');
        return;
    }
    var data = {
        Reviews: reviews,
        ReviewDetails: $("#panel-form input[name='ReviewDetails']").val(),
        CreatedBy: $("#panel-form input[name='CreatedBy']").val(),
        CreatedOn: $("#panel-form input[name='CreatedOn']").val()
    };
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/ReviewPanel/Insert',
        data: data,
        success: function (response) {
            $('#panel-modal').modal('hide');
            window.location.href = response.Url
                ;        }
    });
    return false;
});
$('#panel-modal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
})


$('#btnPanelUpdate').on('click', function () {
    if (!$('#panel-form-edit').valid()) {
        return;
    }
    $('.case-list-msg-edit').html('');
    var caseIds = [];
    var reviews = []
    $('.editHiddenCaseId').each(function (index, val) {
        caseIds.push($(val).val());
    });
    var reviewId = $('#editHiddenReviewId').val();
    caseIds.forEach(caseId => {
        var memberIds = [];
        var isChecked = $('#edit_case_' + caseId).is(':checked');
        if (isChecked) {
            $(`#edit_check_${caseId} .member input[type=checkbox]:checked`).each(function (index, val) {
                var splittedId = $(val).attr('id').split('_');
                var memberId = splittedId[splittedId.length - 1];
                memberIds.push(memberId)
            });
        }
        if (memberIds.length) {
            reviews.push({ CaseID: caseId, Members: memberIds });
        }
    });
    if (!reviews.length) {
        $('.case-list-msg-edit').html('Please select Cases and their panel members');
        return;
    }
    var data = {
        ReviewID: reviewId,
        Reviews: reviews,
        ReviewDetails: $("#panel-form-edit input[name='ReviewDetails']").val(),
        CreatedBy: $("#panel-form-edit input[name='CreatedBy']").val(),
        CreatedOn: $("#panel-form-edit input[name='CreatedOn']").val()
    };
    $.ajax({
        type: 'POST',
        url: '/ReviewPanel/UpdateReviewPanel',
        data: data,
        success: function (response) {
            $('#panel-modal').modal('hide');
            window.location.href = response.Url;
        }
    });
    return false;
});

