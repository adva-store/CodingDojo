<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class() extends Migration {
    public function up(): void
    {
        Schema::create('grids', function (Blueprint $table) {
            $table->id();
            $table->foreignId('ant_id');
            $table->integer('size', false, true);
            $table->integer('move_counter', false, true)->default(0);
            $table->json('colors');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('grids');
    }
};
