export class UserReview {
    userReviewId: number;
    userId: number;
    bookid: number;
    customerReview: string;
    rating: number;
    createdOn: Date;

    constructor (
        userReviewId: number,
        userId: number,
        bookid: number,
        customerReview: string,
        rating: number,
        createdOn: Date,
    ) {
        this.userReviewId = userReviewId;
        this.userId = userId;
        this.bookid = bookid;
        this.customerReview = customerReview;
        this.rating = rating;
        this.createdOn = createdOn;
    }
}
