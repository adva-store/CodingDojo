<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <title>Langton Ant</title>

        @vite(['resources/css/app.css', 'resources/js/app.js'])
    </head>

    <body class="antialiased">
        <div class="max-w-sm mx-auto">
            <div class="flex flex-row flex-wrap">
                <div class="w-1/4 py-4">
                    <form method="POST" action="{{ route('langton-ant-start') }}">
                        @csrf
                        <input type="submit" value="Start" class="border border-slate-500 bg-slate-100 px-4" />
                    </form>

                    @isset($grid)
                        <div class="pt-2">
                            <h6 class="text-xs">Kantenlänge</h6>

                            {{ $grid->size }}
                        </div>

                        <div class="pt-2">
                            <h6 class="text-xs">Startposition</h6>

                            {{ $ant->position_y.'x'.$ant->position_x }}
                        </div>

                        <div class="pt-2">
                            <h6 class="text-xs">Blickrichtung</h6>

                            {{ $ant->direction }}
                        </div>

                        <div class="pt-2">
                            <h6 class="text-xs">Züge</h6>

                            {{ $grid->move_counter }}
                        </div>
                    @endisset
                </div>

                @isset($grid)
                    <div @class(['grid', ' py-4', 'w-3/4', 'grid-cols-11'])>
                        @foreach($grid->colors ?? [] as $rowIndex => $rowValue)
                            @foreach($rowValue ?? [] as $colIndex => $colValue)
                                <div @class(['border', 'border-black', 'bg-black' => $colValue == \App\Models\Grid::BLACK, 'bg-white' => $colValue == \App\Models\Grid::WHITE])>
                                    @if($ant->position_y == $rowIndex && $ant->position_x == $colIndex)
                                        <div @class([
                                            'text-red-500',
                                            'font-bold',
                                            'rotate-90' => $ant->direction == \App\Models\Ant::DIRECTION_LEFT,
                                            '-rotate-90' => $ant->direction == \App\Models\Ant::DIRECTION_RIGHT,
                                            'rotate-180' => $ant->direction == \App\Models\Ant::DIRECTION_TOP
                                        ])>
                                            Y
                                        </div>
                                    @else
                                        &nbsp;
                                    @endif
                                </div>
                            @endforeach
                        @endforeach
                    </div>

                    @empty($end)
                        <script>
                            setTimeout(() => window.location.href = '{{ route('langton-ant-walk', ['id' => $grid->id]) }}', {{ $speed }})
                        </script>
                    @else
                        <div class="mx-auto font-bold">
                            ENDE
                        </div>
                    @endempty
                @endisset
            </div>
        </div>
    </body>
</html>
