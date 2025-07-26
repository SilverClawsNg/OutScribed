function OnGetScroll() {
    var scrollPosition = document.documentElement.scrollTop;

    document.body.classList.add("modal-open");

    return scrollPosition;

}