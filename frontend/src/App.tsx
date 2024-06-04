import {useEffect, useState} from 'react'
import {Flex, Input, List, Pagination} from 'antd'
import './App.css'
import {keepPreviousData, QueryClient, QueryClientProvider, useQuery} from '@tanstack/react-query'

const queryClient = new QueryClient()

export function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <BooksList/>
        </QueryClientProvider>
    )
}

type BooksGetManyRequestDto = {
    page: number,
    perPage: number,
    suggest: string,
    author: string,
    category: string,
}

const booksParamsInit: BooksGetManyRequestDto = {
    page: 1,
    perPage: 5,
    suggest: '',
    author: '',
    category: '',
}

type BookDto = {
    id: string,
    title: string,
    category: string,
    authors: {
        name: string,
        lang: string,
    }[],
    publicationDate: string,
    pages: number,
    ageLimit: number
}

type BookGetManyResponse = {
    items: BookDto[],
    pageInfo: {
        items: number,
        page: number,
        perPage: number,
        pages: number
    }
}

const fetchBooks = async (request: BooksGetManyRequestDto): Promise<BookGetManyResponse> => {
    const endpoint = 'http://localhost:5228/api/books?'

    const requestSearchParams = new URLSearchParams([
        ['page', request.page.toString()],
        ['per_page', request.perPage.toString()],
        ['suggest', request.suggest.toString()],
        ['author', request.author.toString()],
        ['category', request.category.toString()],
    ])

    const response = await fetch(endpoint + new URLSearchParams(requestSearchParams))

    return await response.json();
}

function BooksList() {
    const [booksGetManyRequest, setBooksGetManyRequest] = useState<BooksGetManyRequestDto>(booksParamsInit)

    const booksGetManyRequestDebounced = useDebounce<BooksGetManyRequestDto>(booksGetManyRequest, 200)

    const {data, isFetching} =
        useQuery({
            queryKey: ['books', booksGetManyRequestDebounced],
            queryFn: () => fetchBooks(booksGetManyRequestDebounced),
            placeholderData: keepPreviousData,
        })

    return (
        <List
            header={
                <Flex gap={2}>
                    <Input
                        placeholder='Название...'
                        value={booksGetManyRequest.suggest}
                        onChange={(e) =>
                            setBooksGetManyRequest({...booksGetManyRequest, suggest: e.target.value})}
                    />

                    <Input
                        placeholder='Автор...'
                        value={booksGetManyRequest.author}
                        onChange={(e) =>
                            setBooksGetManyRequest({...booksGetManyRequest, author: e.target.value})}
                    />

                    <Input
                        placeholder='Жанр...'
                        value={booksGetManyRequest.category}
                        onChange={(e) =>
                            setBooksGetManyRequest({...booksGetManyRequest, category: e.target.value})}
                    />
                </Flex>
            }
            footer={
                <Pagination
                    current={booksGetManyRequest.page}
                    defaultCurrent={1}
                    defaultPageSize={5}
                    pageSize={booksGetManyRequest.perPage}
                    pageSizeOptions={[5, 10, 15]}
                    total={data?.pageInfo.items}
                    onChange={(page, pageSize) =>
                        setBooksGetManyRequest({...booksGetManyRequest, page: page, perPage: pageSize})}
                />
            }
            dataSource={data?.items}
            loading={isFetching}
            renderItem={(item: BookDto) => (
                <List.Item>
                    <List.Item.Meta
                        title={
                            <p>{item.title} -- {item.authors.map((x) => `[${x.lang}] ${x.name}`).join(',')}</p>
                        }
                        description={item.category}
                    />
                    Год публикации: {item.publicationDate}, Страниц: {item.pages}, От лет: {item.ageLimit}
                </List.Item>
            )}
        />
    )
}

function useDebounce<T>(value: T, delay: number) {
    const [debouncedValue, setDebouncedValue] = useState(value);

    useEffect(() => {
        const handler = setTimeout(() => {
            setDebouncedValue(value);
        }, delay);
        return () => {
            clearTimeout(handler);
        };
    }, [value, delay]);

    return debouncedValue;
}

export default App
