export class BookLoan {
    loanId: number;
    userId: number;
    bookId: number;
    checkedOut: Date;
    checkedIn: Date;

    constructor (
        loanId: number,
        userId: number,
        bookId: number,
        checkedOut: Date,
        checkedIn: Date,
    ) {
        this.loanId = loanId;
        this.userId = userId;
        this.bookId = bookId;
        this.checkedOut = checkedOut;
        this.checkedIn = checkedIn;
    }
}
