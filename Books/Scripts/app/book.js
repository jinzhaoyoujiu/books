angular.module("booksApp", [])
    .controller("getBooks", function ($scope, $http) {
        $scope.books = [];
        $scope.book = "";
        $scope.bookID = 0;
        $scope.getAllBooks = function () {
            $http.get("api/Books").then(function (data) {
                console.log('data',data);
                $scope.books = data.data;
            });
        };
        $scope.getBook = function (bookID) {
            $http.get("api/Books/" + bookID).then(function (data) {
                $scope.book = data.data;
            });
        };
        $scope.GetBooksByGenre = function (genre) {
            $http.get("api/Books/" + genre).then(function (data) {
                $scope.books = data.data;
            });
        };
        $scope.GetBooksByAuthor = function (authorId) {
            $http.get("api/author/" + authorId+"/books" ).then(function (data) {
                $scope.books = data.data;
            });
        };
        $scope.GetBooksByDate = function (date) {
            $http.get("api/Books/" + date).then(function (data) {
                $scope.books = data.data;
            });
        };
    });