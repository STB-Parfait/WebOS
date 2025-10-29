<script>
</script>

<template>
    <div class="container">
        <div class="card">
            <img src="../../public/placeholder.svg" alt="placeholder.svg"/>
            <h1>WebOS</h1>
            <p>V. 0.0.1</p>
        </div>
        <div class="tab">
            <input type="email" placeholder="email"/><br/>
            <input type="password" placeholder="password"/>
            <button>continue</button><br/>
            <p>login</p>
        </div>
    </div>
    
</template>

<style>
    .container{
        width: fit-content;
    }
    .card{
        width: fit-content;
        padding: 15px;
        background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
        img{
            width: 100px;
            height: 100px;
        }
    }
    h1{
        margin-top: 15px;
        margin-bottom: 5px;
        font-family: "WDXL Lubrifont SC", sans-serif;
        font-weight: 400;
        font-style: normal;
    }
    p{
        margin-top: 5px;
        margin-bottom: 0px;
        margin-left: 0px;
        margin-right: 0px;
        font-family: "WDXL Lubrifont SC", sans-serif;
        font-weight: 400;
        font-style: normal;
    }
    .tab{margin-top: 10px;
        padding: 15px;
        background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
        clip-path: polygon(
            0% 0%,        /* 1. Canto superior esquerdo */
            100% 0%,       /* 2. Canto superior direito */
            100% 70%,      /* 3. Borda direita até 80% */
            30% 70%,       /* 4. Ponto de início da aba (45% da largura) */
            23% 90%,      /* 5. Canto inferior da aba (40% da largura) */
            0% 90%,       /* 6. Canto inferior esquerdo */
            0% 80%         /* 7. Volta para a borda esquerda, na altura de 80% */
        );
        button{
            display: in;
        }
        input{
            margin-bottom: 5px;
            margin-right: 5px;
        }
        p{
            margin-top: 17px;
            margin-bottom: 5px;
            width: fit-content;
        }
    }
</style>