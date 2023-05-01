<?php

use App\Http\Controllers\LangtonAnt;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/', function () {
    return view('langton-ant');
});

Route::post('/langton-ant', [LangtonAnt::class, 'start'])
    ->name('langton-ant-start');

Route::get('/langton-ant/{id}', [LangtonAnt::class, 'walk'])
    ->name('langton-ant-walk');
