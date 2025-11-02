const express = require('express');
const cors = require('cors');
const connectDb = require('./config/database.js');
const userRoutes = require('./routes/UserRoutes.js');
require('dotenv').config();

const app = express();
app.use(express.json());
app.use(cors());
connectDb();

app.use('/api/users', userRoutes);

const PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
    console.log('Server listening on port:', PORT);
});