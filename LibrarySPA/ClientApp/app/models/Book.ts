import { Genre } from './genre'

export class Book {
    constructor(
        public id: number,
        public name: string,
        public author: string,
        public year: number,
        public pubHouse: string,
        public pageCount: number,
        public cost: number,
        public description: string,
        public gener_id: number,
        public genre: Genre
    ) { }
}