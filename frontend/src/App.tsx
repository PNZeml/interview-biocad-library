import {useEffect, useState} from 'react'
import {Flex, Input, List, Pagination} from 'antd'
import './App.css'
import {keepPreviousData, QueryClient, QueryClientProvider, useQuery} from '@tanstack/react-query'

const queryClient = new QueryClient()

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <BooksList/>
        </QueryClientProvider>
    )
}

type BooksGetManyParams = {
    page: number,
    perPage: number,
    suggest: string,
    author: string,
    category: string,
}

const booksParamsInit: BooksGetManyParams = {
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

function BooksList() {
    const [booksParams, setBooksParams] = useState<BooksGetManyParams>(booksParamsInit)

    const booksParamsDebounce = useDebounce<BooksGetManyParams>(booksParams, 200)

    const fetchBooks = async (): Promise<BookGetManyResponse> => {
        const endpoint = 'http://localhost:5228/api/books?'

        const params = new URLSearchParams([
            ['page', booksParamsDebounce.page.toString()],
            ['per_page', booksParamsDebounce.perPage.toString()],
            ['suggest', booksParamsDebounce.suggest.toString()],
            ['author', booksParamsDebounce.author.toString()],
            ['category', booksParamsDebounce.category.toString()],
        ])

        const res = await fetch(endpoint + new URLSearchParams(params))

        return await res.json();
    }

    const {data, isFetching} =
        useQuery({
            queryKey: ['projects', booksParamsDebounce],
            queryFn: () => fetchBooks(),
            placeholderData: keepPreviousData,
        })

    return (
        <List
            header={
                <Flex gap={2}>
                    <Input
                        placeholder='Название...'
                        value={booksParams.suggest}
                        onChange={(e) =>
                            setBooksParams({...booksParams, suggest: e.target.value})}
                    />

                    <Input
                        placeholder='Автор...'
                        value={booksParams.author}
                        onChange={(e) =>
                            setBooksParams({...booksParams, author: e.target.value})}
                    />

                    <Input
                        placeholder='Жанр...'
                        value={booksParams.category}
                        onChange={(e) =>
                            setBooksParams({...booksParams, category: e.target.value})}
                    />
                </Flex>
            }
            footer={
                <Pagination
                    current={booksParams.page}
                    defaultCurrent={1}
                    defaultPageSize={5}
                    pageSize={booksParams.perPage}
                    pageSizeOptions={[5, 10, 15]}
                    total={data?.pageInfo.items}
                    onChange={(page, pageSize) =>
                        setBooksParams({...booksParams, page: page, perPage: pageSize})}
                />
            }
            dataSource={data?.items}
            loading={isFetching}
            renderItem={(item: BookDto) => (
                <List.Item>
                    {item.title}, {item.category}
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
